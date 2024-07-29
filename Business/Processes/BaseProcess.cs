using Data.Extensions;
using Data.Interfaces;
using Data.Services;
using Library.Common;
using Business.Utility;
using System.Web.Mvc;
using Library.Dtos;

namespace Business.Processes
{
    public abstract class BaseProcess<TEntity, TEntityLine, TDto, TDtoLine>
        where TDto : BaseDto<TDtoLine>, new()
        where TDtoLine : BaseLineDto, new()
        where TEntity : class, new()
         where TEntityLine : class, new()
    {
        protected readonly IRepoService repo;
        private readonly IAppSettings _appSetting;
        protected readonly IMapperService mapper;
        private readonly ResponseJson<TDto, TDtoLine> jsonResponse = new();
        public BaseProcess(IRepoService _repo, IAppSettings appSetting, IMapperService _mapper)
        {
            repo = _repo;
            _appSetting = appSetting;
            mapper = _mapper;
        }
        public virtual IEnumerable<TEntity> FilterQuery(string filterType)
        {
            IQueryable<TEntity> model = repo.Query<TEntity>();
            return model;
        }

        public virtual IEnumerable<TEntity> GetAllQuery()
        {
            IQueryable<TEntity> model = repo.Query<TEntity>();
            return model;
        }

        public virtual IQueryable<TEntityLine> FilterLineItemsQuery(string filterType)
        {
            IQueryable<TEntityLine> model = repo.Query<TEntityLine>();
            return model;
        }

        public virtual TEntity Find(string filterType)
        {
            TEntity model = new();
            return model;
        }
        public virtual TDto DefaultValuesGet(TDto dto, bool isNew, string queryParam)
        {
            return dto;
        }
        public virtual TDto DefaultValuesEditGet(TDto dto)
        {
            return dto;
        }


        public virtual TDto LogicBeforePost(TDto dto, bool isNew)
        {
            return dto;
        }

        public virtual TEntity LogicAfterPost(TEntity entity, TDto dto)
        {
            return entity;
        }

        public virtual TDto PopulateSelecTEntitys(TDto dto)
        {
            return dto;
        }


        public virtual async Task<ResponseJson<TDto, TDtoLine>> AddEdit(TDto data)
        {
            try
            {
                TEntity entity = new TEntity();
                data = LogicBeforePost(data, data.NewDto);
                var model = mapper.MapConfig(data, entity);
                model = repo.InsertUpdate(model, data.NewDto);
                if (data.HasLines)
                {
                    model = SaveLines(data, model);
                }
                model = LogicAfterPost(model, data);

                data = mapper.MapDefault<TEntity, TDto>(model);

                //to reload lines where its defined
                data = DefaultValuesEditGet(data);

                jsonResponse.Data = data;
                jsonResponse.Message = "Data saved successfully.";
                jsonResponse.IsSuccess = true;
                return jsonResponse;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                jsonResponse.Data = data;
                jsonResponse.Message = ExceptionUtility.NavException(ex);
                jsonResponse.IsSuccess = false;
                return jsonResponse;
            }
        }


        public TEntity SaveLines(TDto dto, TEntity entity)
        {
            if (!dto.HasLines || dto.DtoLines == null || !dto.DtoLines.Any())
                return entity;

            foreach (var line in dto.DtoLines)
            {
                TEntityLine model = new();
                if (!line.NewDtoLine && !string.IsNullOrEmpty(line.LineId))
                {
                    model = mapper.MapConfig(line, model);
                    repo.InsertUpdate(model, line.NewDtoLine);
                }
                else if (line.NewDtoLine)
                {
                    model = mapper.MapConfig(line, model);
                    repo.InsertUpdate(model, line.NewDtoLine);
                }
            }
            return entity;
        }

        public virtual ResponseJson<TDto, TDtoLine> Delete(string id)
        {
            try
            {
                var entity = new TEntity();
                if (entity == null)
                {
                    jsonResponse.Message = "Record to delete not found.";
                    jsonResponse.IsSuccess = false;
                    return jsonResponse;
                }
                else
                {
                    repo.Delete(entity);
                    jsonResponse.Message = "Record deleted.";
                    jsonResponse.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                jsonResponse.Message = ExceptionUtility.NavException(ex);
                jsonResponse.IsSuccess = false;
            }
            return jsonResponse;
        }

    }
}
