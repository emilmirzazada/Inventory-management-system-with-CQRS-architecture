using Dapper;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.DTOs.Claims;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Dapper;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDapper dapper;

        public RoleRepository(IDapper dapper)
        {
            this.dapper = dapper;
        }

        public void ManageRoleClaims(RoleClaimsViewModel model)
        {
            
            try
            {
                model.Claims = model.Claims.Where(c => c.IsSelected).ToList();
                DynamicParameters p = new DynamicParameters();
                StringBuilder sql = new StringBuilder(@"Declare @Claims as UT_RoleClaimsa
                            insert into @Claims values");
                if (model.Claims.Count==0)
                {
                    sql = new StringBuilder(@"Declare @Claims as UT_RoleClaimsa ");
                }
                else
                {
                    for (int i = 0; i < model.Claims.Count; i++)
                    {
                        p.Add("ClaimType"+i, model.Claims[i].ClaimType);
                        p.Add("ClaimValue"+i, model.Claims[i].ClaimValue);

                        sql.Append($"(@ClaimType{i},@ClaimValue{i}),");
                    }
                }
                sql.Remove(sql.Length - 1, 1);
                
                p.Add("RoleId", model.RoleId);
                sql.AppendLine("exec dbo.USP_UPDATEROLECLAIMS @Claims, @RoleId");
                dapper.Execute(sql.ToString(), p, CommandType.Text);
            }
            catch (Exception exception)
            {

                throw exception;
            }

        }
    }
}
