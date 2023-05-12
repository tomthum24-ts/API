
using API.APPLICATION.Commands.Location.SyncLocation;
using API.APPLICATION.ViewModels.Location;
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.Location;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands
{
    public class SyncLocationCommandHandler : IRequestHandler<SyncLocationCommand, MethodResult<SyncLocationCommandResponse>>
    {
        //static HttpClient client = new HttpClient();
        private IHttpClientFactory _factory;
        private IUserSessionInfo _userSessionInfo;
        protected readonly IProvinceRepository _provinceRepository;
        protected readonly IDistrictRepository _districtRepository;
        protected readonly IVillageRepository _villageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SyncLocationCommandHandler(IHttpClientFactory factory, IUserSessionInfo userSessionInfo, IProvinceRepository provinceRepository, IUnitOfWork unitOfWork, IMapper mapper, IDistrictRepository districtRepository, IVillageRepository villageRepository)
        {
            _factory = factory;
            _userSessionInfo = userSessionInfo;
            _provinceRepository = provinceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _districtRepository = districtRepository;
            _villageRepository = villageRepository;
        }

        public async Task<MethodResult<SyncLocationCommandResponse>> Handle(SyncLocationCommand request, CancellationToken cancellationToken)
        {
            var Id = _userSessionInfo.ID.GetValueOrDefault();
            var methodResult = new MethodResult<SyncLocationCommandResponse>();
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri("http://provinces.open-api.vn");
            var response = client.GetAsync("/api/?depth="+ request.PhanLoai).Result;
            var jsonData = response.Content.ReadAsStringAsync().Result;
            List<LocationReponseViewModel> data = JsonSerializer.Deserialize<List<LocationReponseViewModel>>(jsonData);
            foreach (var item_Provice in data)
            {
                //Check existingProvince
                var existingProvince = await _provinceRepository.Get(x => x.ProvinceCode == item_Provice.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                if (existingProvince == null) //Not existing addNew
                {
                    var province = new Province(item_Provice.code.ToString(),
                                               item_Provice.name,
                                               item_Provice.codename,
                                               item_Provice.division_type,
                                               null,
                                               true);
                    _provinceRepository.Add(province);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    foreach (var item_District in item_Provice.districts)
                    {
                        var existingDistrict = await _districtRepository.Get(x => x.DistrictCode == item_District.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                        if (existingDistrict == null)  //Not existing addNew
                        {
                            var district = new District(item_District.code.ToString(),
                                                    item_District.name,
                                                    item_District.codename,
                                                    item_District.division_type,
                                                    province.Id,
                                                    null,
                                                    true
                            );
                            _districtRepository.Add(district);
                             await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                            foreach (var item_Village in item_District.wards)
                            {
                                var existingVillage = await _villageRepository.Get(x => x.VillageCode == item_Village.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                                if (existingVillage == null) //Not existing addNew
                                {
                                    var village = new Village(item_Village.code.ToString(),
                                                    item_Village.name,
                                                    item_Village.codename,
                                                    item_Village.division_type,
                                                    district.Id,
                                                    null,
                                                    true
                                                    );
                                    _villageRepository.Add(village);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }
                                else // existing update
                                {
                                    existingVillage.SetVillageCode(item_Village.code.ToString());
                                    existingVillage.SetVillageName(item_Village.name);
                                    existingVillage.SetCodeName(item_Village.codename);
                                    existingVillage.SetIdDistrict(district.Id);
                                    _villageRepository.Update(existingVillage);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }

                            }
                        }
                        else  // existing update
                        {
                            existingDistrict.SetDistrictCode(item_District.code.ToString());
                            existingDistrict.SetDistrictName(item_District.name);
                            existingDistrict.SetCodeName(item_District.codename);
                            existingDistrict.SetDivisionType(item_District.division_type);
                            existingDistrict.SetIdProvince(existingProvince.Id);
                            _districtRepository.Update(existingDistrict);
                            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                            foreach (var item_Village in item_District.wards)
                            {
                                var existingVillage = await _villageRepository.Get(x => x.VillageCode == item_Village.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                                if (existingVillage == null) //Not existing addNew
                                {
                                    var village = new Village(item_Village.code.ToString(),
                                                    item_Village.name,
                                                    item_Village.codename,
                                                    item_Village.division_type,
                                                    existingDistrict.Id,
                                                    null,
                                                    true
                                                    );
                                    _villageRepository.Add(village);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }
                                else // existing update
                                {
                                    existingVillage.SetVillageCode(item_Village.code.ToString());
                                    existingVillage.SetVillageName(item_Village.name);
                                    existingVillage.SetCodeName(item_Village.codename);
                                    existingVillage.SetDivisionType(item_Village.division_type);
                                    existingVillage.SetIdDistrict(existingDistrict.Id);
                                    _villageRepository.Update(existingVillage);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }

                            }
                        }

                    }
                }
                else // Existing update
                {
                    existingProvince.SetProvinceCode(item_Provice.code.ToString());
                    existingProvince.SetProvinceName(item_Provice.name);
                    existingProvince.SetCodeName(item_Provice.codename);
                    existingProvince.SetDivisionType(item_Provice.division_type);
                    _provinceRepository.Update(existingProvince);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    foreach (var item_District in item_Provice.districts)
                    {
                        var existingDistrict = await _districtRepository.Get(x => x.DistrictCode == item_District.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                        if (existingDistrict == null)  //Not existing addNew
                        {
                            var district = new District(item_District.code.ToString(),
                                                    item_District.name,
                                                    item_District.codename,
                                                    item_District.division_type,
                                                    existingProvince.Id,
                                                    null,
                                                    true
                            );
                            _districtRepository.Add(district);
                            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                            foreach (var item_Village in item_District.wards)
                            {
                                var existingVillage = await _villageRepository.Get(x => x.VillageCode == item_Village.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                                if (existingVillage == null) //Not existing addNew
                                {
                                    var village = new Village(item_Village.code.ToString(),
                                                    item_Village.name,
                                                    item_Village.codename,
                                                    item_Village.division_type,
                                                    district.Id,
                                                    null,
                                                    true
                                                    );
                                    _villageRepository.Add(village);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }
                                else // existing update
                                {
                                    existingVillage.SetVillageCode(item_Village.code.ToString());
                                    existingVillage.SetVillageName(item_Village.name);
                                    existingVillage.SetCodeName(item_Village.codename);
                                    existingVillage.SetIdDistrict(district.Id);
                                    _villageRepository.Update(existingVillage);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }

                            }
                        }
                        else  // existing update
                        {
                            existingDistrict.SetDistrictCode(item_District.code.ToString());
                            existingDistrict.SetDistrictName(item_District.name);
                            existingDistrict.SetCodeName(item_District.codename);
                            existingDistrict.SetDivisionType(item_District.division_type);
                            existingDistrict.SetIdProvince(existingProvince.Id);
                            _districtRepository.Update(existingDistrict);
                            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                            foreach (var item_Village in item_District.wards)
                            {
                                var existingVillage = await _villageRepository.Get(x => x.VillageCode == item_Village.code.ToString()).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                                if (existingVillage == null) //Not existing addNew
                                {
                                    var village = new Village(item_Village.code.ToString(),
                                                    item_Village.name,
                                                    item_Village.codename,
                                                    item_Village.division_type,
                                                    existingDistrict.Id,
                                                    null,
                                                    true
                                                    );
                                    _villageRepository.Add(village);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }
                                else // existing update
                                {
                                    existingVillage.SetVillageCode(item_Village.code.ToString());
                                    existingVillage.SetVillageName(item_Village.name);
                                    existingVillage.SetCodeName(item_Village.codename);
                                    existingVillage.SetDivisionType(item_Village.division_type);
                                    existingVillage.SetIdDistrict(existingDistrict.Id);
                                    _villageRepository.Update(existingVillage);
                                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                }

                            }
                        }

                    }
                }


            }
            return methodResult;
        }
    }
}
