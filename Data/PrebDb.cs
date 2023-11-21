using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Interface;
using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public static class PrebDb
    {
        public static void Prepulation( this IApplicationBuilder app){

            using (var serviceScope = app.ApplicationServices.CreateScope())

            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.ReturnAllPlatforms();    

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(),platforms);
            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms){

                Console.WriteLine("Seedng new platforms...");
                foreach (var plat in platforms)
                {
                    if(!repo.ExternalPlatformExist(plat.ExternalID));
                        {
                            repo.CreatePlatform(plat);
                        }
                }

        }
    }
}