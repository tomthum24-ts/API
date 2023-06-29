
using API.APPLICATION.Parameters.Menu;
using API.DOMAIN.DTOs.Menu;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Menu
{
    public interface  IMenuServices
    {
        Task<IEnumerable<MenuDTO>> GetListMenuAsync(MenuFilterParam param);
        Task<IEnumerable<MenuCapMotDTO>> GetListMenuTestAsync(MenuFilterParam param);
    }
    public class MenuServices : IMenuServices
    {
        public readonly DapperContext _context;


        public MenuServices(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuDTO>> GetListMenuAsync(MenuFilterParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetListMenu", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<MenuDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<MenuCapMotDTO>> GetListMenuTestAsync(MenuFilterParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetListMenu", param, commandType: CommandType.StoredProcedure);
            var rawData = await rs.ReadAsync<MenuDTO>().ConfigureAwait(false);
            IEnumerable<MenuCapMotDTO> result = rawData?.GroupBy(x => x?.C1_Id)?
                                                    .Select(y => new MenuCapMotDTO()
                                                    {
                                                        C1_Id = y.First().C1_Id,
                                                        C1_Name = y.First().C1_Name,
                                                        C1_Image = y.First().C1_Image,
                                                        C1_Link = y.First().C1_Link,
                                                        C1_IdParent=y.First().C1_IdParent,
                                                        MenuCapHais= rawData?.Where(e =>  e.C2_IdParent== y.First().C1_Id).GroupBy(c=>c?.C2_Id)?.Select(z=> new MenuCapHaiDTO
                                                        {
                                                            C2_Id = z.First().C2_Id,
                                                            C2_Name=z.First().C2_Name,
                                                            C2_Image=z.First().C2_Image,
                                                            C2_Link=z.First().C2_Link,
                                                            C2_IdParent=z.First().C2_IdParent,
                                                            MenuCapBas= rawData?.Where(f=>f.C3_IdParent == z.First().C2_Id).GroupBy(g=> g?.C3_Id )?.Select(t=> new MenuCapBaDTO
                                                            {
                                                               C3_Id = z.First().C3_Id,
                                                               C3_Name=t.First().C3_Name,
                                                               C3_Image=t.First().C3_Image,
                                                               C3_Link=t.First().C3_Link,
                                                               C3_IdParent=t.First().C3_IdParent,
                                                            })
                                                        }
                                                        )
                                                    });
            return result;
        }
    }
}
