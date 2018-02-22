using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bilisimHR.Common.Core.AutoMapping
{
    public class AutoMapperGenericHelper<TSource, TDestination>
    {
        public static TDestination Convert(TSource model)
        {
            try
            {
                if (Mapper.Instance.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() == null)
                {
                    return Mapper.Instance.Map<TSource, TDestination>(model);
                }

                return Mapper.Map<TSource, TDestination>(model);
            }
            catch (InvalidOperationException ex)
            {
                //Eğer mapper initalize edilmemişse Mapper.Instance InvalidOperationException fırlatmaktadır. İlk olarak initialize etmek için eklendi.
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                    cfg.CreateMissingTypeMaps = true;
                    cfg.ForAllMaps((mapType, mapperExpression) =>
                    {
                        mapperExpression.MaxDepth(1);
                    });
                });

                return Mapper.Map<TSource, TDestination>(model);
            }
        }

        public static IQueryable<TDestination> ConvertAsQueryable(IQueryable<TSource> sourceQueryable)
        {
            // BAD IDEA
            List<TSource> listSource = sourceQueryable.ToList();
            List<TDestination> listDestination = new List<TDestination>();

            foreach (TSource sourceObj in listSource)
                listDestination.Add(Convert(sourceObj));

            return listDestination.AsQueryable();
                      
            // return sourceQueryable.Select(source => Convert(source)).AsQueryable();

            #region ProjectTo not working
            try
            {
                if (Mapper.Instance.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() == null)
                {
                    Mapper.Initialize(cfg => {
                        cfg.CreateMap<TSource, TDestination>();
                        cfg.CreateMissingTypeMaps = true;
                        cfg.ForAllMaps((mapType, mapperExpression) => {
                            mapperExpression.MaxDepth(1);
                        });
                    });
                }
                    

                return sourceQueryable.AsEnumerable().Select(source => Mapper.Map<TSource, TDestination>(source)).AsQueryable();
                // return sourceQueryable.ProjectTo<TDestination>();
            }
            catch (InvalidOperationException)
            {
                //Eğer mapper initalize edilmemişse Mapper.Instance InvalidOperationException fırlatmaktadır. İlk olarak initialize etmek için eklendi.
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                    cfg.CreateMissingTypeMaps = true;

                    cfg.ForAllMaps((mapType, mapperExpression) =>
                    {
                        mapperExpression.MaxDepth(1);
                    });
                });

                return sourceQueryable.AsEnumerable().Select(source => Mapper.Map<TSource, TDestination>(source)).AsQueryable();
                // return sourceQueryable.ProjectTo<TDestination>();
            }
            #endregion
        }

        public static IList<TDestination> ConvertAsList(IList<TSource> sourceQueryable)
        {
            try
            {
                if (Mapper.Instance.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() == null)
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<TSource, TDestination>();
                        cfg.CreateMissingTypeMaps = true;
                        cfg.ForAllMaps((mapType, mapperExpression) =>
                        {
                            mapperExpression.MaxDepth(1);
                        });
                    });

                return Mapper.Map<IList<TSource>, IList<TDestination>>(sourceQueryable);
            }
            catch (InvalidOperationException)
            {
                //Eğer mapper initalize edilmemişse Mapper.Instance InvalidOperationException fırlatmaktadır. İlk olarak initialize etmek için eklendi.
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                    cfg.CreateMissingTypeMaps = true;

                    cfg.ForAllMaps((mapType, mapperExpression) =>
                    {
                        mapperExpression.MaxDepth(1);
                    });
                });

                return Mapper.Map<IList<TSource>, IList<TDestination>>(sourceQueryable);
            }
            //IEnumerable<TDestination> list = sourceQueryable.Select(source => Convert(source));
            //return list.ToList();
        }
    }
}
