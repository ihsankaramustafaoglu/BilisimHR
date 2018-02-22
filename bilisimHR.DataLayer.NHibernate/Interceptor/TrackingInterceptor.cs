using bilisimHR.Common.Helper.ServiceLocator;
using bilisimHR.Common.Helper.ServiceLocator.RevisionInfo;
using NHibernate;
using System;
using System.Linq;

namespace bilisimHR.DataLayer.NHibernate.Interceptor
{
    public class TrackingInterceptor : EmptyInterceptor
    {
      
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, global::NHibernate.Type.IType[] types)
        {
            if (!propertyNames.Any("InsertedBy".Contains))
                return base.OnSave(entity, id, state, propertyNames, types);

            var time = DateTime.Now;
            int userId = ServiceLocator.GetService<IRevisionInfoService>().UserId;


            var indexOfCreatedBy = GetIndex(propertyNames, "InsertedBy");
            var indexOfCreated = GetIndex(propertyNames, "InsertedDate");
            var indexOfModifiedBy = GetIndex(propertyNames, "UpdatedBy");
            var indexOfModified = GetIndex(propertyNames, "UpdatedDate");


            state[indexOfCreatedBy] = userId;
            state[indexOfCreated] = time;
            state[indexOfModifiedBy] = userId;
            state[indexOfModified] = time;
            
            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, global::NHibernate.Type.IType[] types)
        {
            var time = DateTime.Now;

            int userId = ServiceLocator.GetService<IRevisionInfoService>().UserId;

            var indexOfCreatedBy = GetIndex(propertyNames, "InsertedBy");
            var indexOfCreated = GetIndex(propertyNames, "InsertedDate");
            var indexOfModifiedBy = GetIndex(propertyNames, "UpdatedBy");
            var indexOfModified = GetIndex(propertyNames, "UpdatedDate");

            //var typedEntity = (Entity<ulong>)entity;
            //if (typedEntity.CreatedDate == DateTime.MinValue)
            //{
            //    currentState[indexOfCreated] = time;
            //    currentState[indexOfCreatedBy] = userId;
            //    typedEntity.CreatedBy = userId;
            //    typedEntity.CreatedDate = time;
            //}

            currentState[indexOfCreatedBy] = userId;
            currentState[indexOfCreated] = time;
            currentState[indexOfModifiedBy] = userId;
            currentState[indexOfModified] = time;
            
            
            //typedEntity.UpdatedBy = userId;
            //typedEntity.UpdatedDate = time;

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        private int GetIndex(object[] array, string property)
        {
            for (var i = 0; i < array.Length; i++)
                if (array[i].ToString() == property)
                    return i;

            return -1;
        }
    }
}
