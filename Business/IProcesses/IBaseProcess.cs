using System;
using Library.Common;
using Library.Dtos;

namespace Business.IProcesses
{
    public interface IBaseProcess<TEntity, TEntityLine, TDto, TDtoLine>
       where TDto : BaseDto<TDtoLine>, new()
        where TDtoLine : BaseLineDto, new()
        where TEntity : class, new()
         where TEntityLine : class, new()
    {
        IEnumerable<TEntity> FilterQuery(string filterType);
        IEnumerable<TEntity> GetAllQuery();
        IQueryable<TEntityLine> FilterLineItemsQuery(string filterType);
        TEntity Find(string filterType);
        TDto DefaultValuesGet(TDto dto, bool isNew, string queryParam);
        TDto DefaultValuesEditGet(TDto dto);
        TDto LogicBeforePost(TDto dto, bool isNew);
        TEntity LogicAfterPost(TEntity entity, TDto dto);
        Task<ResponseJson<TDto, TDtoLine>> AddEdit(TDto data);
        TEntity SaveLines(TDto dto, TEntity entity);
        ResponseJson<TDto, TDtoLine> Delete(string id);
    }
}
