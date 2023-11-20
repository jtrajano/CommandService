using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Models;

namespace CommandService.Interface
{
    public interface ICommandRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExist(int platformId);
        bool ExternalPlatformExist(int externalPlatformId);


        IEnumerable<Command> GetCommandsForPlatfrom( int platformId);
        Command GetCommand(int platformId, int commandId);

        void CreateCommand(int platformId, Command command);

    }
}