using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Dtos;
using CommandService.Interface;
using CommandService.Models;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
   
     
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
      
        
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    //todo
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
                
                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExist(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine("Platform Added!");
                    }   
                    else
                    {
                        Console.WriteLine("--> Platform already exists..");
                    } 
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"--> Could not add the Platform to DB{ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {


            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published" : 
                    Console.WriteLine("Platform Published Event Detected");
                    return EventType.PlatformPublished;
                    
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
            

        }
    }

    public enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}