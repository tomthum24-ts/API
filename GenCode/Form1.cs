using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenCode
{
    public partial class Form1 : Form
    {
        string connectString = "server=DESKTOP-2LILVUD\\SQLEXPRESS;database=NetCore;uid=sa;password=Tienson0204;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            txtModule.Text = "API";
            txtRefix.Text = "API";
            TxtNameSpace.Text = "API";
            txtThuMuc.Text = Application.StartupPath;
            TxtOutput.ScrollBars = ScrollBars.Both;
            TxtConnectString.Text = connectString;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //string value = TxtConnectString.Text;
            
            string value = connectString;
            DataAccess context = new DataAccess(value);

            string[] arrParameterNames = new string[] { "Refix" };

            object[] arrValues = new object[] { txtRefix.Text.Trim() };

            var _ds = context.ExcuteSelectIsStore("SP_AD_GetAllTable", arrParameterNames, arrValues);

            if (_ds == null || _ds.Tables.Count == 0)
            {
                return;
            }

            if (_ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            string thumuc = txtThuMuc.Text;
            if (string.IsNullOrEmpty(thumuc))
            {
                thumuc = Application.StartupPath;
            }
            try
            {
                if (Directory.Exists(thumuc + @"\Sources"))
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(thumuc + @"\Sources");
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thư mục đang được sử dụng " + ex.Message);
            }

            if (!Directory.Exists(thumuc + @"\Sources"))
            {
                Directory.CreateDirectory(thumuc + @"\Sources");
            }
            thumuc = thumuc + @"\Sources";


            // Project API
            string thumucAPI = thumuc + @"\API";
            if (!Directory.Exists(thumucAPI))
            {
                Directory.CreateDirectory(thumucAPI);
            }

            // Project DOMAIN
            string thumucDomain = thumuc + @"\DOMAIN";
            if (!Directory.Exists(thumucDomain))
            {
                Directory.CreateDirectory(thumucDomain);
            }

            // Project DOMAIN
            string thumucInfrastructure = thumuc + @"\INFRASTRUCTURE";
            if (!Directory.Exists(thumucInfrastructure))
            {
                Directory.CreateDirectory(thumucInfrastructure);
            }

            // Object set

            #region Object set

            StringBuilder strObject = new StringBuilder();
            StringBuilder strInterface = new StringBuilder();
            StringBuilder strContans = new StringBuilder();
            StringBuilder strEnumCode = new StringBuilder();
            StringBuilder strApplicationModule = new StringBuilder();
            StringBuilder strMappings = new StringBuilder();
            foreach (DataRow item in _ds.Tables[0].Rows)
            {
                if (item["ObjectSet"] != DBNull.Value)
                {
                    strObject.AppendLine(item["ObjectSet"].ToString());
                }

                if (item["Interface"] != DBNull.Value)
                {
                    strInterface.AppendLine(item["Interface"].ToString());
                }

                if (item["Contans"] != DBNull.Value)
                {
                    strContans.AppendLine(item["Contans"].ToString());
                }


                string[] arrParameterNamesTb = new string[] { "TableName", "Refix" };

                object[] arrValuesTb = new object[] { item["TbName"].ToString(), txtRefix.Text.Trim() };

                // Build class Entity
                var dsTb = context.ExcuteSelectIsStore("SP_AD_GetAllColumnsObject", arrParameterNamesTb, arrValuesTb);

                if (dsTb == null || dsTb.Tables.Count == 0)
                {
                    continue;
                }

                if (dsTb.Tables[0].Rows.Count == 0)
                {
                    continue;
                }
                string tenbangRep = item["NameReplace"].ToString();
                StringBuilder strConfig = new StringBuilder();
                var classObj = GenDomainObjects(dsTb.Tables[0], tenbangRep, strEnumCode, ref strConfig);
                if (!Directory.Exists(thumucDomain + @"\DomainObject"))
                {
                    Directory.CreateDirectory(thumucDomain + @"\DomainObject");
                }
                using (StreamWriter writetext = new StreamWriter(string.Format(thumucDomain + @"\DomainObject\{0}.cs", tenbangRep)))
                {
                    writetext.WriteLine(classObj.ToString());
                }


                #region Repository

                #endregion

                #region EFConfigs

                StringBuilder strEFConfigs = new StringBuilder();
                strEFConfigs.AppendLine(@"using API." + txtRefix.Text.ToUpper() + @".DOMAIN;
                                            using Microsoft.EntityFrameworkCore;
                                            using Microsoft.EntityFrameworkCore.Metadata.Builders;");

                strEFConfigs.AppendLine("namespace API." + txtRefix.Text.ToUpper() + @".INFRASTRUCTURE.EFConfigs");
                strEFConfigs.AppendLine("{");
                strEFConfigs.AppendLine("public class " + tenbangRep + "Configuration : IEntityTypeConfiguration<" + tenbangRep + ">");
                strEFConfigs.AppendLine("{");
                strEFConfigs.AppendLine("public void Configure(EntityTypeBuilder<" + tenbangRep + "> builder)");
                strEFConfigs.AppendLine("{");
                strEFConfigs.AppendLine("builder.ToTable(" + txtRefix.Text.ToUpper() + "Constants." + tenbangRep + "_TABLENAME);");
                strEFConfigs.AppendLine(strConfig.ToString());
                strEFConfigs.AppendLine("}");
                strEFConfigs.AppendLine("}");
                strEFConfigs.AppendLine("}");

                if (!Directory.Exists(thumucInfrastructure + @"\EFConfigs"))
                {
                    Directory.CreateDirectory(thumucInfrastructure + @"\EFConfigs");
                }
                using (StreamWriter writetext = new StreamWriter(string.Format(thumucInfrastructure + @"\EFConfigs\{0}Configuration.cs", tenbangRep)))
                {
                    writetext.WriteLine(strEFConfigs.ToString());
                }
                if (RdEFConfigs.Checked)
                {

                    TxtOutput.Text = strEFConfigs.ToString();
                }
                #endregion

                #region Application Commands



                GenCommandsAndController(dsTb.Tables[0], tenbangRep, txtRefix.Text.Trim().ToUpper(), thumucAPI);


                #endregion

                #region Infrastructure

                // ApplicationModule
                #region ApplicationModule

                strApplicationModule.AppendLine("builder.RegisterType<" + tenbangRep + "Repository>().As<I" + tenbangRep + "Repository>().InstancePerLifetimeScope();");

                #endregion

                #region Mappings

                strMappings.AppendLine("");
                strMappings.AppendLine("public class " + tenbangRep + "Profile : Profile");
                strMappings.AppendLine("{");
                strMappings.AppendLine("public " + tenbangRep + "Profile()");
                strMappings.AppendLine("{");
                strMappings.AppendLine("CreateMap<" + tenbangRep + ", Update" + tenbangRep + "CommandResponse>();");
                strMappings.AppendLine("CreateMap<" + tenbangRep + ", Create" + tenbangRep + "CommandResponse>();");
                strMappings.AppendLine("CreateMap<" + tenbangRep + ", Delete" + tenbangRep + "CommandResponse>();");
                strMappings.AppendLine("CreateMap<" + tenbangRep + ", " + tenbangRep + "ViewModel>();");
                strMappings.AppendLine("CreateMap<" + tenbangRep + ", " + tenbangRep + "Command>();");
                strMappings.AppendLine("}");
                strMappings.AppendLine("}");

                strMappings.AppendLine("");
                if (RdMapper.Checked)
                {
                    TxtOutput.Text = strMappings.ToString();
                }
                #endregion

                #endregion

                #region Queries

                StringBuilder strIQueries = new StringBuilder();
                strIQueries.AppendLine(@"using BaseCommon.Common.MethodResult;
                                        using Services.Common.Paging;
                                        using System.Threading.Tasks;");

                strIQueries.AppendLine("namespace API." + txtRefix.Text.ToUpper() + @".APPLICATION.Queries");
                strIQueries.AppendLine("{");
                strIQueries.AppendLine("public interface I" + tenbangRep + "Queries");
                strIQueries.AppendLine("{");
                strIQueries.AppendLine("Task<MethodResult<" + tenbangRep + "ViewModel>> Get" + tenbangRep + "ByIdAsync(GetByIdParam param);");
                strIQueries.AppendLine("Task<MethodResult<PagingItems<" + tenbangRep + "ViewModel>>> GetDataListAsync(GetListParam request);");
                strIQueries.AppendLine("}");
                strIQueries.AppendLine("}");

                if (!Directory.Exists(thumucAPI + @"\Queries/IRepositories"))
                {
                    Directory.CreateDirectory(thumucAPI + @"\Queries/IRepositories");
                }
                using (StreamWriter writetext = new StreamWriter(string.Format(thumucAPI + @"\Queries/IRepositories/I{0}Queries.cs", tenbangRep)))
                {
                    writetext.WriteLine(strIQueries.ToString());
                }

                // Reponsity
                StringBuilder strQueries = new StringBuilder();
                strQueries.AppendLine(@"using API." + txtRefix.Text.ToUpper() + @".DOMAIN;
                                        using AutoMapper;
                                        using Dapper;
                                        using Dapper.Common.Services;
                                        using Microsoft.AspNetCore.Http;
                                        using Microsoft.EntityFrameworkCore;
                                        using Services.Common.DI;
                                        using Services.Common.Domain.Repositories;
                                        using Services.Common.Domain.Uow;
                                        using BaseCommon.Common.MethodResult;
                                        using Services.Common.Paging;
                                        using System.Data;
                                        using System.Threading.Tasks;
                                        ");

                strQueries.AppendLine("namespace API." + txtRefix.Text.ToUpper() + @".APPLICATION.Queries");
                strQueries.AppendLine("{");
                strQueries.AppendLine("[ScopedDependency(ServiceType = typeof(I" + tenbangRep + "Queries))]");
                strQueries.AppendLine("public class " + tenbangRep + "Queries : I" + tenbangRep + "Queries");
                strQueries.AppendLine("{");
                strQueries.AppendLine("protected readonly IDapperDbConnectionFactory _connectionFactory;");
                strQueries.AppendLine("protected readonly IHttpContextAccessor _httpContextAccessor;");
                strQueries.AppendLine("protected readonly IEntityRepository<" + tenbangRep + ", int> _entityRepos;");
                strQueries.AppendLine("protected readonly IMapper _mapper;");
                strQueries.AppendLine("public " + tenbangRep + "Queries(IDapperDbConnectionFactory connectionFactory, IHttpContextAccessor httpContextAccessor,IUnitOfWork _unitOfWork,IMapper mapper)");
                strQueries.AppendLine("{");
                strQueries.AppendLine("_entityRepos = _unitOfWork.GetEntityRepository<" + tenbangRep + ", int>();");
                strQueries.AppendLine("_connectionFactory = connectionFactory;");
                strQueries.AppendLine("_httpContextAccessor = httpContextAccessor;");
                strQueries.AppendLine("_mapper = mapper;");
                strQueries.AppendLine("}");
                strQueries.AppendLine("");
                strQueries.AppendLine("public async Task<MethodResult<" + tenbangRep + "ViewModel>> Get" + tenbangRep + "ByIdAsync(GetByIdParam param)");
                strQueries.AppendLine("{");
                strQueries.AppendLine("var methodResult = new MethodResult<" + tenbangRep + "ViewModel>();");
                strQueries.AppendLine("var result = await _entityRepos.Get(x => x.Id == param.Id).SingleOrDefaultAsync();");
                strQueries.AppendLine("methodResult.Result = _mapper.Map<" + tenbangRep + "ViewModel>(result);");
                strQueries.AppendLine("return methodResult;");
                strQueries.AppendLine("}");
                strQueries.AppendLine("");
                strQueries.AppendLine("public async Task<MethodResult<PagingItems<" + tenbangRep + "ViewModel>>> GetDataListAsync(GetListParam request)");
                strQueries.AppendLine("{");
                strQueries.AppendLine("var methodResult = new MethodResult<PagingItems<" + tenbangRep + "ViewModel>>");
                strQueries.AppendLine("{");
                strQueries.AppendLine("Result = new PagingItems<" + tenbangRep + "ViewModel>");
                strQueries.AppendLine("{");
                strQueries.AppendLine("PagingInfo = new PagingInfo { PageNumber = request.PageNumber, PageSize = request.PageSize }");
                strQueries.AppendLine("}");
                strQueries.AppendLine("};");
                strQueries.AppendLine("request.TableName = " + txtRefix.Text + "Constants." + tenbangRep + "_TABLENAME;");
                strQueries.AppendLine("request.DsColName = " + txtRefix.Text + "Extension.GetColumnTableName(typeof(" + tenbangRep + "ViewModel));");
                strQueries.AppendLine("var conn = _connectionFactory.GetDbConnection();");
                strQueries.AppendLine("using var rs = await conn.QueryMultipleAsync(\"SP_DM_GetDanhMuc\", request, commandType: CommandType.StoredProcedure);");
                strQueries.AppendLine("methodResult.Result.Items = await rs.ReadAsync<" + tenbangRep + "ViewModel>().ConfigureAwait(false);");
                strQueries.AppendLine("methodResult.Result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);");
                strQueries.AppendLine("return methodResult;");
                strQueries.AppendLine("}");


                strQueries.AppendLine("}");
                strQueries.AppendLine("}");

                if (!Directory.Exists(thumucAPI + @"\Queries\Repositories"))
                {
                    Directory.CreateDirectory(thumucAPI + @"\Queries\Repositories");
                }
                using (StreamWriter writetext = new StreamWriter(string.Format(thumucAPI + @"\Queries\Repositories\{0}Queries.cs", tenbangRep)))
                {
                    writetext.WriteLine(strQueries.ToString());
                }


                #endregion

                #region ViewModels

                StringBuilder strViewModels = new StringBuilder();
                strViewModels.AppendLine(@"using System;
                                            using System.Collections.Generic;
                                            using System.Linq;
                                            using System.Threading.Tasks;");

                strViewModels.AppendLine("namespace API." + txtRefix.Text.ToUpper() + @".APPLICATION");
                strViewModels.AppendLine("{");
                strViewModels.AppendLine("public class " + tenbangRep + "ViewModel : " + tenbangRep + "Command");
                strViewModels.AppendLine("{");
                strViewModels.AppendLine("}");
                strViewModels.AppendLine("}");

                if (!Directory.Exists(thumucAPI + @"\ViewModels"))
                {
                    Directory.CreateDirectory(thumucAPI + @"\ViewModels");
                }
                using (StreamWriter writetext = new StreamWriter(string.Format(thumucAPI + @"\ViewModels\{0}ViewModel.cs", tenbangRep)))
                {
                    writetext.WriteLine(strViewModels.ToString());
                }

                #endregion


            }

            #region DomainPlicy

            StringBuilder strDomainDTOs = new StringBuilder();

            strDomainDTOs.Append(@"using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Text.Json.Serialization;
                                using System.Threading.Tasks;");
            strDomainDTOs.AppendLine();
            strDomainDTOs.Append("namespace API." + txtRefix.Text.Trim() + ".DOMAIN.DTOs");
            strDomainDTOs.AppendLine();
            strDomainDTOs.Append(@"
                                {
                                    public class ExcuteRequestViewModel
                                    {
                                        [JsonIgnore]
                                        public int IdUser { get; set; }
                                        [JsonIgnore]
                                        public string IdsThamChieu { get; set; }

                                        [JsonIgnore]
                                        public string TableName { get; set; }

                                    }

                                    public class ExcuteReponseViewModel
                                    {
                                        [JsonIgnore]
                                        public int Id { get; set; }
                                        [JsonIgnore]
                                        public string ErrorCode { get; set; }

                                        [JsonIgnore]
                                        public string ErrorMesage { get; set; }
                                        [JsonIgnore]
                                        public string ArrValue { get; set; }
                                    }
                                }
                                ");


            StringBuilder strIDomainPlicy = new StringBuilder();

            strIDomainPlicy.Append("using API." + txtRefix.Text.Trim() + ".DOMAIN.DTOs;");
            strIDomainPlicy.AppendLine();
            strIDomainPlicy.Append(@"
                                    using Services.Common.Domain.Services;
                                    using System.Collections.Generic;
                                    using System.Threading.Tasks;");
            strIDomainPlicy.AppendLine();
            strIDomainPlicy.Append("namespace API." + txtRefix.Text.Trim() + ".DOMAIN.Services");
            strIDomainPlicy.AppendLine();
            strIDomainPlicy.Append(@"
                                {
                                    public interface IDomainCustomPolicy : IDomainService
                                    {
                                        Task<bool> CheckDataIsUseAsync(ExcuteRequestViewModel request);
                                        void DeleteReferencesAsync(ExcuteRequestViewModel request);
                                    }
                                }
                             ");



            StringBuilder strPolicy = new StringBuilder();

            strPolicy.Append("using API." + txtRefix.Text.Trim() + ".DOMAIN");
            strPolicy.AppendLine();
            strPolicy.Append("using API." + txtRefix.Text.Trim() + ".DOMAIN.DTOs");
            strPolicy.AppendLine();
            strPolicy.Append("using API." + txtRefix.Text.Trim() + ".DOMAIN.Services");
            strPolicy.AppendLine();

            strPolicy.Append(@"using Dapper;
                            using Dapper.Common.Services;
                            using Services.Common.DI;
                            using Services.Common.DomainObjects.Exceptions;
                            using BaseCommon.Common.MethodResult;
                            using System;
                            using System.Collections.Generic;
                            using System.Data;
                            using System.Linq;
                            using System.Text;
                            using System.Threading.Tasks;");
            strPolicy.AppendLine();
            strPolicy.Append("namespace API." + txtRefix.Text.Trim() + ".INFRASTRUCTURE.Services");
            strPolicy.AppendLine();
            strPolicy.Append(@"
                              
{
    [ScopedDependency(ServiceType = typeof(IDomainCustomPolicy))]
    public class DomainCustomPolicy : IDomainCustomPolicy
    {
        private readonly IDapperDbConnectionFactory _connectionFactory;

        public DomainCustomPolicy(IDapperDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CheckDataIsUseAsync(ExcuteRequestViewModel request)
        {
            bool isvalid = false;
            var conn = _connectionFactory.GetDbConnection();");
            strPolicy.AppendLine();
            strPolicy.Append("var rs = await conn.QueryMultipleAsync(\"SP_" + txtRefix.Text.Trim() + "_CheckDataUsing\", new { TableName = request.TableName, IdsThamChieu = request.IdsThamChieu }, commandType: CommandType.StoredProcedure);");
            strPolicy.AppendLine();
            strPolicy.Append(@"
            var result = await rs.ReadAsync<ExcuteReponseViewModel>().ConfigureAwait(false);
            if (result != null && result.Any(m => !string.IsNullOrEmpty(m.ErrorCode)))
            {
                var objErr = result.FirstOrDefault();
                ErrorResult err = new ErrorResult()
                {
                    ErrorCode = objErr.ErrorCode,
                    ErrorMessage = objErr.ErrorMesage,
                    ErrorValues = objErr.ArrValue.Split('|').ToList()

                };

                throw new DomainException(err);
            }
            else
            {
                isvalid = true;
            }
            return isvalid;
        }

        public async void DeleteReferencesAsync(ExcuteRequestViewModel request)
        {
            var conn = _connectionFactory.GetDbConnection();");
            strPolicy.AppendLine();
            strPolicy.Append("await conn.QueryMultipleAsync(\"SP_" + txtRefix.Text.Trim() + "_DeleteAllReferences\", new { TableName = request.TableName, IdsThamChieu = request.IdsThamChieu, IdUser = request.IdUser }, commandType: CommandType.StoredProcedure);");
            strPolicy.AppendLine();
            strPolicy.Append(@"
                }
            }
        }

        ");


            #endregion

            //using (StreamWriter writetext = new StreamWriter(thumuc + @"\Orthers/ApplicationModule.cs"))
            //{
            //    writetext.WriteLine(strApplicationModule.ToString());
            //}

            using (StreamWriter writetext = new StreamWriter(thumucAPI + @"\Mappings.cs"))
            {
                writetext.WriteLine(strMappings.ToString());
            }

            using (StreamWriter writetext = new StreamWriter(thumucDomain + @"\" + txtRefix.Text.Trim() + "EnumCode.cs"))
            {
                writetext.WriteLine(strEnumCode.ToString());
            }




            using (StreamWriter writetext = new StreamWriter(thumucDomain + @"\" + "DomainCustomDTO.cs"))
            {
                writetext.WriteLine(strDomainDTOs.ToString());
            }

            using (StreamWriter writetext = new StreamWriter(thumucDomain + @"\" + "IDomainCustomPolicy.cs"))
            {
                writetext.WriteLine(strIDomainPlicy.ToString());
            }

            using (StreamWriter writetext = new StreamWriter(thumucInfrastructure + @"\Context.cs"))
            {
                writetext.WriteLine(strObject.ToString());
            }

            using (StreamWriter writetext = new StreamWriter(thumucInfrastructure + @"\DomainCustomPolicy.cs"))
            {
                writetext.WriteLine(strPolicy.ToString());
            }

            //using (StreamWriter writetext = new StreamWriter(thumuc + @"\Orthers/Interface.cs"))
            //{
            //    writetext.WriteLine(strInterface.ToString());
            //}
            using (StreamWriter writetext = new StreamWriter(thumucDomain + @"\Contans.cs"))
            {
                writetext.WriteLine(strContans.ToString());
            }

            GC.Collect();

            #endregion

            GC.Collect();

            System.Diagnostics.Process.Start("explorer.exe", thumuc);
        }
        private void GenCommandsAndController(DataTable dt, string objectName, string module, string path)
        {

            if (!Directory.Exists(path + @" / Commands"))
            {
                Directory.CreateDirectory(path + @"\Commands");
            }
            if (!Directory.Exists(path + @"\Commands/" + objectName))
            {
                Directory.CreateDirectory(path + @"\Commands/" + objectName);
            }
            if (!Directory.Exists(path + @"\Commands/" + objectName + @"\BaseClasses"))
            {
                Directory.CreateDirectory(path + @"\Commands/" + objectName + @"\BaseClasses");
            }

            if (!Directory.Exists(path + @"\Controllers"))
            {
                Directory.CreateDirectory(path + @"\Controllers");
            }
            string strSetContructor = "";
            string strSetUpdate = "";
            string strDim = "";

            #region BaseClasses Command

            StringBuilder strBD = new StringBuilder();
            string propertyall = "";
            strBD.AppendLine(@"using System;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class " + objectName + "Command");
            strBD.AppendLine("{");
            strBD.AppendLine("public int Id { get; set; }");

            string codeForObject = "";
            #region Properties

            foreach (DataRow item in dt.Rows)
            {
                if (item["ColumnName"] != DBNull.Value)
                {
                    string col = item["ColumnName"].ToString();
                    if (col.StartsWith("Ma"))
                    {
                        codeForObject = col;
                    }

                    propertyall += "public " + item["ColumnType"].ToString() + item["NullableSign"].ToString() + " " + col + " { get; set;}" + Environment.NewLine;

                    strSetContructor += strDim + "request." + col;
                    strSetUpdate += "objExist.Set" + col + "(request." + col + ");" + Environment.NewLine;
                    strDim = ", ";
                }
            }
            strBD.AppendLine(propertyall);
            strBD.AppendLine("}");
            strBD.AppendLine("}");

            #endregion

            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\BaseClasses\{0}Command.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Create Command

            strBD = new StringBuilder();
            strBD.AppendLine(@" using MediatR;
                                using BaseCommon.Common.MethodResult;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Create" + objectName + "Command : IRequest<MethodResult<Create" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");
            strBD.AppendLine(propertyall);
            strBD.AppendLine("}");

            strBD.AppendLine("public class Create" + objectName + "CommandResponse : " + objectName + "Command");
            strBD.AppendLine("{");
            strBD.AppendLine("}");

            strBD.AppendLine("}");

            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Create{0}Command.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Create Command Handler

            strBD = new StringBuilder();
            strBD.AppendLine(@"using API." + module + @".DOMAIN;
                                using AutoMapper;
                                using MediatR;
                                using Microsoft.AspNetCore.Http;
                                using Microsoft.EntityFrameworkCore;
                                using System.Threading;
                                using BaseCommon.Common.MethodResult;
                                using API.Extension;
                                using BaseCommon.UnitOfWork;
                                using System.Threading.Tasks;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Create" + objectName + "CommandHandler : " + objectName + "IRequestHandler<Create" + objectName + "Command, MethodResult<Create" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");

            strBD.AppendLine("private readonly IHttpContextAccessor _httpContextAccessor;");
            strBD.AppendLine("private readonly IMapper _mapper;");
            strBD.AppendLine("private readonly IUnitOfWork _unitOfWork;");
            strBD.AppendLine("private readonly I"+ objectName + "Repository _contextRepository ;");


            strBD.AppendLine("public Create" + objectName + "CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) ");
            strBD.AppendLine("{");
            strBD.AppendLine("_httpContextAccessor = httpContextAccessor;");
            strBD.AppendLine("_unitOfWork = unitOfWork;");
            strBD.AppendLine("_contextRepository = unitOfWork;");
            strBD.AppendLine("}");

            strBD.AppendLine("public async Task<MethodResult<Create" + objectName + "CommandResponse>> Handle(Create" + objectName + "Command request, CancellationToken cancellationToken)");
            strBD.AppendLine("{");

            strBD.AppendLine("var methodResult = new MethodResult<Create" + objectName + "CommandResponse>();");
            strBD.AppendLine("var newObject = new " + objectName + "(" + strSetContructor + "); ");
            strBD.AppendLine("/*Check exists*/");
            strBD.AppendLine("");

            if (string.IsNullOrEmpty(codeForObject))
            {
                strBD.AppendLine("/*");
            }

            strBD.AppendLine("if (!await _contextRepository.Get(x => x." + codeForObject + " == newObject." + codeForObject + " && x.Id != newObject.Id && (!x.IsDelete.HasValue || x.IsDelete == false)).AnyAsync(cancellationToken: cancellationToken))");
            strBD.AppendLine("{");
            strBD.AppendLine("methodResult.AddAPIErrorMessage(nameof(E" + objectName + "ErrorCode.E" + txtRefix.Text + "EXIST), new[]");
            strBD.AppendLine("{");
            strBD.AppendLine("ErrorHelpers.GenerateErrorResult(nameof(newObject." + codeForObject + "),newObject." + codeForObject + ")");
            strBD.AppendLine("});");
            strBD.AppendLine("}");

            if (string.IsNullOrEmpty(codeForObject))
            {
                strBD.AppendLine("*/");
            }

            strBD.AppendLine("if (!methodResult.IsOk) throw new CommandHandlerException(methodResult.ErrorMessages);");

            strBD.AppendLine("_contextRepository.Add(newObject);");
            strBD.AppendLine("await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);");
            strBD.AppendLine("methodResult.Result = _mapper.Map<Create" + objectName + "CommandResponse>(newObject);");
            strBD.AppendLine("return methodResult;");

            strBD.AppendLine("}");

            strBD.AppendLine("}");

            strBD.AppendLine("}");
            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Create{0}CommandHandler.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Update Command

            strBD = new StringBuilder();
            strBD.AppendLine(@" using MediatR;
                                using BaseCommon.Common.MethodResult;
                                using System;
                                using System.Collections.Generic;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Update" + objectName + "Command : IRequest<MethodResult<Update" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");
            strBD.AppendLine("public int Id { get; set; }");
            strBD.AppendLine(propertyall);
            strBD.AppendLine("}");

            strBD.AppendLine("public class Update" + objectName + "CommandResponse : " + objectName + "Command");
            strBD.AppendLine("{");
            strBD.AppendLine("}");

            strBD.AppendLine("}");

            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Update{0}Command.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Update Command Handler

            strBD = new StringBuilder();
            strBD.AppendLine(@"using API." + module + @".DOMAIN;
                                using AutoMapper;
                                using MediatR;
                                using Microsoft.AspNetCore.Http;
                                using Microsoft.EntityFrameworkCore;
                                using Services.Common.Domain.Uow;
                                using Services.Common.DomainObjects.Exceptions;
                                using BaseCommon.Common.MethodResult;
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Threading;
                                using System.Threading.Tasks;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Update" + objectName + "CommandHandler : " + objectName + "CommandHandler, IRequestHandler<Update" + objectName + "Command, MethodResult<Update" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");
            strBD.AppendLine("private readonly IHttpContextAccessor _httpContextAccessor;");


            strBD.AppendLine("public Update" + objectName + "CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork)");
            strBD.AppendLine("{");
            strBD.AppendLine("_httpContextAccessor = httpContextAccessor;");
            strBD.AppendLine("}");

            strBD.AppendLine("public async Task<MethodResult<Update" + objectName + "CommandResponse>> Handle(Update" + objectName + "Command request, CancellationToken cancellationToken)");
            strBD.AppendLine("{");

            strBD.AppendLine("var methodResult = new MethodResult<Update" + objectName + "CommandResponse>();");
            strBD.AppendLine("/*Check exists, error*/");
            strBD.AppendLine("");
            strBD.AppendLine("var objExist = await _contextRepository.Get(x => x.Id == request.Id  && (!x.IsDelete.HasValue || x.IsDelete == false)).SingleOrDefaultAsync(cancellationToken: cancellationToken);");

            strBD.AppendLine("if (objExist == null)");
            strBD.AppendLine("{");

            strBD.AppendLine("methodResult.AddAPIErrorMessage(nameof(E" + objectName + "ErrorCode.E" + txtRefix.Text + "EMPTY), new[]");
            strBD.AppendLine("{");
            strBD.AppendLine("ErrorHelpers.GenerateErrorResult(nameof(request.Id),request.Id)");
            strBD.AppendLine("});");
            strBD.AppendLine("}");

            if (string.IsNullOrEmpty(codeForObject))
            {
                strBD.AppendLine("/*");
            }
            strBD.AppendLine("else if (!await _contextRepository.Get(x => x." + codeForObject + " == objExist." + codeForObject + " && x.Id != objExist.Id && (!x.IsDelete.HasValue || x.IsDelete == false)).AnyAsync(cancellationToken: cancellationToken))");
            strBD.AppendLine("{");

            strBD.AppendLine("methodResult.AddAPIErrorMessage(nameof(E" + objectName + "ErrorCode.E" + txtRefix.Text + "EXIST), new[]");
            strBD.AppendLine("{");
            strBD.AppendLine("ErrorHelpers.GenerateErrorResult(nameof(request.Id),request.Id)");
            strBD.AppendLine("});");
            strBD.AppendLine("}");

            if (string.IsNullOrEmpty(codeForObject))
            {
                strBD.AppendLine("*/");
            }
            strBD.AppendLine("if (!methodResult.IsOk) throw new CommandHandlerException(methodResult.ErrorMessages);");

            strBD.AppendLine(strSetUpdate);
            strBD.AppendLine(" _contextRepository.Update(objExist);");
            strBD.AppendLine("await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);");
            strBD.AppendLine("methodResult.Result = _mapper.Map<Update" + objectName + "CommandResponse>(objExist);");
            strBD.AppendLine("return methodResult;");

            strBD.AppendLine("}");
            strBD.AppendLine("}");
            strBD.AppendLine("}");
            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Update{0}CommandHandler.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Delete Command

            strBD = new StringBuilder();
            strBD.AppendLine(@"using MediatR;
                            using BaseCommon.Common.MethodResult;
                            using System.Collections.Generic;");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Delete" + objectName + "Command : IRequest<MethodResult<Delete" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");
            strBD.AppendLine("public List<int> Ids { get; set; }");
            strBD.AppendLine("}");

            strBD.AppendLine("public class Delete" + objectName + "CommandResponse");
            strBD.AppendLine("{");

            strBD.AppendLine("public Delete" + objectName + "CommandResponse(List<" + objectName + "Command> datas)");
            strBD.AppendLine("{");
            strBD.AppendLine("Datas = datas;");
            strBD.AppendLine("}");
            strBD.AppendLine("public List<" + objectName + "Command> Datas { get; }");
            strBD.AppendLine("}");

            strBD.AppendLine("}");

            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Delete{0}Command.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
            }


            #endregion

            #region Delete Command Handler

            strBD = new StringBuilder();
            strBD.AppendLine(@"using API." + module + @".DOMAIN;
                                using AutoMapper;
                                using MediatR;
                                using Microsoft.AspNetCore.Http;
                                using Microsoft.EntityFrameworkCore;
                                using Services.Common.Domain.Uow;
                                using Services.Common.DomainObjects.Exceptions;
                                using BaseCommon.Common.MethodResult;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Threading;
                                using System.Threading.Tasks;
                    ");

            strBD.AppendLine("namespace API." + module + ".APPLICATION");
            strBD.AppendLine("{");
            strBD.AppendLine("public class Delete" + objectName + "CommandHandler : " + objectName + "CommandHandler, IRequestHandler<Delete" + objectName + "Command, MethodResult<Delete" + objectName + "CommandResponse>>");
            strBD.AppendLine("{");
            strBD.AppendLine("private readonly IHttpContextAccessor _httpContextAccessor;");


            strBD.AppendLine("public Delete" + objectName + "CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork)");
            strBD.AppendLine("{");
            strBD.AppendLine("_httpContextAccessor = httpContextAccessor;");
            strBD.AppendLine("}");

            strBD.AppendLine("public async Task<MethodResult<Delete" + objectName + "CommandResponse>> Handle(Delete" + objectName + "Command request, CancellationToken cancellationToken)");
            strBD.AppendLine("{");

            strBD.AppendLine("var methodResult = new MethodResult<Delete" + objectName + "CommandResponse>();");
            strBD.AppendLine("/*Check exists, error*/");
            strBD.AppendLine("");
            strBD.AppendLine("var lstDatas = await _contextRepository.Get(x => request.Ids.Contains(x.Id) && (!x.IsDelete.HasValue || x.IsDelete == false)).ToListAsync(cancellationToken: cancellationToken);");
            strBD.AppendLine("if (lstDatas == null || !lstDatas.Any())");
            strBD.AppendLine("{");
            strBD.AppendLine("/*");
            strBD.AppendLine("methodResult.AddAPIErrorMessage(nameof(E" + objectName + "ErrorCode.E" + txtRefix.Text + "EMPTY), new[]");
            strBD.AppendLine("{");
            strBD.AppendLine("ErrorHelpers.GenerateErrorResult(nameof(request.Ids),string.Join(',',request.Ids))");
            strBD.AppendLine("});");
            strBD.AppendLine("*/");
            strBD.AppendLine("}");

            strBD.AppendLine("if (!methodResult.IsOk) throw new CommandHandlerException(methodResult.ErrorMessages);");

            strBD.AppendLine("_contextRepository.DeleteRange(lstDatas);");
            strBD.AppendLine("await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);");
            strBD.AppendLine("var dataResponses = _mapper.Map<List<" + objectName + "Command>>(lstDatas);");
            strBD.AppendLine("methodResult.Result = new Delete" + objectName + "CommandResponse(dataResponses);");
            strBD.AppendLine("return methodResult;");

            strBD.AppendLine("}");
            strBD.AppendLine("}");
            strBD.AppendLine("}");
            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Commands/" + objectName + @"\Delete{0}CommandHandler.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());

            }


            #endregion

            #region Controllers

            strBD = new StringBuilder();
            strBD.AppendLine(@"
                        using API.DOMAIN;
                        using AutoMapper;
                        using BaseCommon.Attributes;
                        using BaseCommon.Common.MethodResult;
                        using BaseCommon.Common.Response;
                        using BaseCommon.Model;
                        using MediatR;
                        using Microsoft.AspNetCore.Authorization;
                        using Microsoft.AspNetCore.Mvc;
                        using System.Collections.Generic;
                        using System.Linq;
                        using System.Net;
                        using System.Threading;
                        using System.Threading.Tasks;");

            strBD.AppendLine("namespace API." + module );
            strBD.AppendLine("{");
            strBD.AppendLine("[Authorize]");
            strBD.AppendLine("[ApiController]");
            strBD.AppendLine("[Route(\"[controller]\")]");
            strBD.AppendLine("public class " + objectName + "Controller : ControllerBase");
            strBD.AppendLine("{");
            strBD.AppendLine("private const string GetById = nameof(GetById);");
            strBD.AppendLine("private const string GetList = nameof(GetList);");
            strBD.AppendLine("private readonly I" + objectName + "Queries _queires;");
            strBD.AppendLine("private readonly IMediator _mediator;");
            strBD.AppendLine("private readonly IMapper _mapper;");
            strBD.AppendLine("public " + objectName + "Controller(IMediator mediator, I" + objectName + "Queries queires, IMapper mapper)");
            strBD.AppendLine("{");
            strBD.AppendLine("_mediator = mediator;");
            strBD.AppendLine("_queires = queires;");
            strBD.AppendLine("_mapper = mapper;");

            strBD.AppendLine("}");

            strBD.AppendLine("/// <summary>");
            strBD.AppendLine("/// Create a new " + objectName + ".");
            strBD.AppendLine("/// </summary>");
            strBD.AppendLine("/// <param name=\"command>\"</param>");
            strBD.AppendLine("/// <returns></returns>");
            strBD.AppendLine("[HttpPost]");
            strBD.AppendLine("[ProducesResponseType(typeof(MethodResult<Create" + objectName + "CommandResponse>), (int)HttpStatusCode.OK)]");
            strBD.AppendLine("[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]");
            strBD.AppendLine("public async Task<IActionResult> Create" + objectName + "Async(Create" + objectName + "Command command)");
            strBD.AppendLine("{");
            strBD.AppendLine("var result = await _mediator.Send(command).ConfigureAwait(false);");
            strBD.AppendLine("return Ok(result);");
            strBD.AppendLine("}");
            strBD.AppendLine("");

            strBD.AppendLine("/// <summary>");
            strBD.AppendLine("/// Update a existing " + objectName + ".");
            strBD.AppendLine("/// </summary>");
            strBD.AppendLine("/// <param name=\"command>\"</param>");
            strBD.AppendLine("/// <returns></returns>");
            strBD.AppendLine("[HttpPut]");
            strBD.AppendLine("[ProducesResponseType(typeof(MethodResult<Update" + objectName + "CommandResponse>), (int)HttpStatusCode.OK)]");
            strBD.AppendLine("[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]");
            strBD.AppendLine("public async Task<IActionResult> Update" + objectName + "Async(Update" + objectName + "Command command)");
            strBD.AppendLine("{");
            strBD.AppendLine("var result = await _mediator.Send(command).ConfigureAwait(false);");
            strBD.AppendLine("return Ok(result);");
            strBD.AppendLine("}");
            strBD.AppendLine("");

            strBD.AppendLine("/// <summary>");
            strBD.AppendLine("/// Delete a existing " + objectName + ".");
            strBD.AppendLine("/// </summary>");
            strBD.AppendLine("/// <param name=\"command>\"</param>");
            strBD.AppendLine("/// <returns></returns>");
            strBD.AppendLine("[HttpDelete]");
            strBD.AppendLine("[ProducesResponseType(typeof(MethodResult<Delete" + objectName + "CommandResponse>), (int)HttpStatusCode.OK)]");
            strBD.AppendLine("[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]");
            strBD.AppendLine("public async Task<IActionResult> Delete" + objectName + "Async(Delete" + objectName + "Command command)");
            strBD.AppendLine("{");
            strBD.AppendLine("var result = await _mediator.Send(command).ConfigureAwait(false);");
            strBD.AppendLine("return Ok(result);");
            strBD.AppendLine("}");
            strBD.AppendLine("");

            strBD.AppendLine("/// <summary>");
            strBD.AppendLine("/// Get " + objectName + " by id.");
            strBD.AppendLine("/// </summary>");
            strBD.AppendLine("/// <param name=\"param>\"</param>");
            strBD.AppendLine("/// <returns></returns>");
            strBD.AppendLine("[HttpPost]");
            strBD.AppendLine("[Route(GetById)]");
            strBD.AppendLine("[ProducesResponseType(typeof(MethodResult<" + objectName + "ViewModel>), (int)HttpStatusCode.OK)]");
            strBD.AppendLine("[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]");
            strBD.AppendLine("public async Task<IActionResult> Get" + objectName + "ByIdAsync(GetByIdParam param)");
            strBD.AppendLine("{");
            strBD.AppendLine("var result = await _queires.Get" + objectName + "ByIdAsync(param);");
            strBD.AppendLine("return Ok(result);");
            strBD.AppendLine("}");
            strBD.AppendLine("");

            /* [AllowAnonymous]*/
            strBD.AppendLine("/// <summary>");
            strBD.AppendLine("/// Get list " + objectName + ".");
            strBD.AppendLine("/// </summary>");
            strBD.AppendLine("/// <param name=\"param>\"</param>");
            strBD.AppendLine("/// <returns></returns>");
            strBD.AppendLine("[HttpPost]");
            strBD.AppendLine("[Route(GetList)]");
            strBD.AppendLine("[ProducesResponseType(typeof(MethodResult<PagingItems<" + objectName + "ViewModel>>), (int)HttpStatusCode.OK)]");
            strBD.AppendLine("[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]");
            strBD.AppendLine("public async Task<IActionResult> GetDataListAsync(GetListParam param)");
            strBD.AppendLine("{");
            strBD.AppendLine("var result = await _queires.GetDataListAsync(param);");
            strBD.AppendLine("return Ok(result);");
            strBD.AppendLine("}");
            strBD.AppendLine("");


            strBD.AppendLine("}");
            strBD.AppendLine("}");

            using (StreamWriter writetext = new StreamWriter(string.Format(path + @"\Controllers\{0}sControllers.cs", objectName)))
            {
                writetext.WriteLine(strBD.ToString());
                if (RdController.Checked)
                {
                    TxtOutput.Text = strBD.ToString();
                }
            }


            #endregion
        }

        private string GenDomainObjects(DataTable dt, string objectName, StringBuilder strEnumCode, ref StringBuilder strConfig)
        {
            StringBuilder strFull = new StringBuilder();
            string usinglib = @"using Services.Common.DomainObjects.Exceptions;
                                using System.ComponentModel.DataAnnotations;";

            strFull.AppendLine(usinglib);
            strFull.AppendLine();
            strFull.AppendLine("namespace API." + txtModule.Text.ToUpper() + ".DOMAIN");
            strFull.AppendLine("{");

            strFull.AppendLine("public class " + objectName + " : " + txtModule.Text.ToUpper() + "Entity");
            strFull.AppendLine("{");
            strFull.AppendLine();

            StringBuilder strFields = new StringBuilder();
            StringBuilder strProperty = new StringBuilder();
            StringBuilder strBehaviours = new StringBuilder();
            StringBuilder strConstructors = new StringBuilder();

            #region Fields
            strFields.AppendLine("#region Fields");
            foreach (DataRow item in dt.Rows)
            {
                if (item["ColumnName"] != DBNull.Value)
                {
                    string col = item["ColumnName"].ToString();
                    string val = "private " + item["ColumnType"].ToString() + item["NullableSign"].ToString() + " _" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + ";";

                    strFields.AppendLine(val);

                    if (item["NullableSign"].ToString() == "")
                    {
                        strConfig.AppendLine("builder.Property(x => x." + col + ").HasField(\"" + "_" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + "\").HasMaxLength(" + item["max_length"].ToString() + ").UsePropertyAccessMode(PropertyAccessMode.Field);");
                    }
                    else
                    {
                        strConfig.AppendLine("builder.Property(x => x." + col + ").HasField(\"" + "_" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + "\").UsePropertyAccessMode(PropertyAccessMode.Field);");
                    }
                }

            }
            strFields.AppendLine("#endregion Fields");

            #endregion


            strFull.AppendLine(strFields.ToString());
            strFull.AppendLine();
            #region Constructors
            strConstructors.AppendLine("#region Constructors");
            strConstructors.AppendLine("public " + objectName + "() {}");
            strConstructors.AppendLine();


            string sdim = "";
            string param = "";
            string setinit = "";
            foreach (DataRow item in dt.Rows)
            {
                if (item["ColumnName"] != DBNull.Value)
                {
                    string col = item["ColumnName"].ToString();
                    param += sdim + item["ColumnType"].ToString() + item["NullableSign"].ToString() + " " + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + "";
                    setinit += "_" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + " = " + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + ";" + Environment.NewLine;
                    sdim = ", ";
                }
            }

            strConstructors.AppendLine("public " + objectName + "(" + param + ")");
            strConstructors.AppendLine("{");
            strConstructors.AppendLine(setinit);
            strConstructors.AppendLine("if (!IsValid()) throw new DomainException(_errorMessages);");
            strConstructors.AppendLine("}");

            strConstructors.AppendLine("#endregion Constructors");

            #endregion


            strFull.AppendLine(strConstructors.ToString());
            strFull.AppendLine();
            #region Properties

            //string errcode = "";
            //// to display the resulted character array 
            //foreach (char s in objectName)
            //{
            //    if((int)s <97)
            //    {
            //        errcode += s;
            //    }
            //}

            strEnumCode.AppendLine("public enum E" + objectName + "ErrorCode");
            strEnumCode.AppendLine("{");
            strEnumCode.AppendLine("E" + txtRefix.Text + "EXIST,");
            strEnumCode.AppendLine("E" + txtRefix.Text + "EMPTY,");

            strProperty.AppendLine("#region Properties");
            foreach (DataRow item in dt.Rows)
            {
                if (item["ColumnName"] != DBNull.Value)
                {
                    string col = item["ColumnName"].ToString();

                    string stype = item["ColumnType"].ToString().Contains("string") ? "[MaxLength(" + item["max_length"].ToString() + ", ErrorMessage = nameof(E" + objectName + "ErrorCode." + item["ColumnId"].ToString() + "))]" : "";
                    string val = "public " + item["ColumnType"].ToString() + item["NullableSign"].ToString() + " " + col + " { get=> _" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + ";}";
                    if (!string.IsNullOrEmpty(stype))
                    {
                        strProperty.AppendLine(stype);
                    }
                    strProperty.AppendLine(val);
                    strEnumCode.AppendLine(item["ColumnId"].ToString() + ",");
                }
            }
            strEnumCode.AppendLine("}");
            strProperty.AppendLine("#endregion Properties");

            #endregion


            strFull.AppendLine(strProperty.ToString());
            strFull.AppendLine();
            #region Behaviours

            strBehaviours.AppendLine("#region Behaviours");
            foreach (DataRow item in dt.Rows)
            {
                if (item["ColumnName"] != DBNull.Value)
                {
                    strBehaviours.AppendLine("");
                    string col = item["ColumnName"].ToString();
                    strBehaviours.AppendLine("public void Set" + col + "(" + item["ColumnType"].ToString() + item["NullableSign"].ToString() + " " + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + ")");
                    strBehaviours.AppendLine("{");
                    strBehaviours.AppendLine(" _" + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + " = " + col.Substring(0, 1).ToLower() + col.Substring(1, col.Length - 1) + ";");
                    if (item["NullableSign"].ToString() == "?")
                    {
                        strBehaviours.AppendLine("if (!IsValid()) throw new DomainException(_errorMessages);");
                    }
                    strBehaviours.AppendLine("}");
                }
            }
            strBehaviours.AppendLine("");
            strBehaviours.AppendLine("public sealed override bool IsValid()");
            strBehaviours.AppendLine("{");
            strBehaviours.AppendLine("return base.IsValid();");
            strBehaviours.AppendLine("}");

            strBehaviours.AppendLine("#endregion Behaviours");

            #endregion

            strFull.AppendLine(strBehaviours.ToString());
            strFull.AppendLine("}");
            strEnumCode.AppendLine("");
            strFull.AppendLine("}");
            return strFull.ToString();
        }
    }
}
