using System;
using Xunit;
using AutoMapper;
using AutoMapper.Configuration;


namespace Proyect.TakuGames.Test.Helpers
{
    public class MapperFixture : IDisposable
    {
        public MapperFixture()
        {
            // var mappings = new MapperConfigurationExpression();
            // mappings.AddProfile<Project.TakuGames.Model.ViewModels.AutomapperProfile>();
            // // Mapper.Initialize(mappings);
            // // mapper = Mapper.Instance;     
           var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<Project.TakuGames.Model.ViewModels.AutomapperProfile>();
            });
            mapper = config.CreateMapper();
            // or
            // var mapper = new Mapper(config);     
        }

        public void Dispose()
        {
            //  Mapper.Reset();            
        }


        

        public IMapper mapper { get; private set; }
    }


    [CollectionDefinition("Mapper Collection")]
    public class MapperCollection : ICollectionFixture<MapperFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }



}
