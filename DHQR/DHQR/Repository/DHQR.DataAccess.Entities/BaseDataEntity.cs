﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace DHQR.DataAccess.Entities
{
    // 捕获需要针对自跟踪实体执行的大部分更改跟踪工作的
    //帮助器类。
    [DataContract(IsReference = true)]
    public class ObjectChangeTracker
    {
        #region  Fields
    
        private bool _isDeserializing;
        private ObjectState _objectState = ObjectState.Added;
        private bool _changeTrackingEnabled;
        private OriginalValuesDictionary _originalValues;
        private ExtendedPropertiesDictionary _extendedProperties;
        private ObjectsAddedToCollectionProperties _objectsAddedToCollections = new ObjectsAddedToCollectionProperties();
        private ObjectsRemovedFromCollectionProperties _objectsRemovedFromCollections = new ObjectsRemovedFromCollectionProperties();
    
        #endregion
    
        #region Events
    
        public event EventHandler<ObjectStateChangingEventArgs> ObjectStateChanging;
    
        #endregion
    
        protected virtual void OnObjectStateChanging(ObjectState newState)
        {
            if (ObjectStateChanging != null)
            {
                ObjectStateChanging(this, new ObjectStateChangingEventArgs(){ NewState = newState });
            }
        }
    
        [DataMember]
        public ObjectState State
        {
            get { return _objectState; }
            set
            {
                if (_isDeserializing || _changeTrackingEnabled)
                {
                    OnObjectStateChanging(value);
                    _objectState = value;
                }
            }
        }
    
        public bool ChangeTrackingEnabled
        {
            get { return _changeTrackingEnabled; }
            set { _changeTrackingEnabled = value; }
        }
    
        // 将已删除对象返回到已更改的集合值属性。
        [DataMember]
        public ObjectsRemovedFromCollectionProperties ObjectsRemovedFromCollectionProperties
        {
            get
            {
                if (_objectsRemovedFromCollections == null)
                {
                    _objectsRemovedFromCollections = new ObjectsRemovedFromCollectionProperties();
                }
                return _objectsRemovedFromCollections;
            }
        }
    
        // 返回已更改的属性的原始值。
        [DataMember]
        public OriginalValuesDictionary OriginalValues
        {
            get
            {
                if (_originalValues == null)
                {
                    _originalValues = new OriginalValuesDictionary();
                }
                return _originalValues;
            }
        }
    
        // 返回扩展属性值。
        // 这包括实体框架中的并发模型所需的
        // 独立关联的键值
        [DataMember]
        public ExtendedPropertiesDictionary ExtendedProperties
        {
            get
            {
                if (_extendedProperties == null)
                {
                    _extendedProperties = new ExtendedPropertiesDictionary();
                }
                return _extendedProperties;
            }
        }
    
        // 将已添加对象返回到已更改的集合值属性。
        [DataMember]
        public ObjectsAddedToCollectionProperties ObjectsAddedToCollectionProperties
        {
            get
            {
                if (_objectsAddedToCollections == null)
                {
                    _objectsAddedToCollections = new ObjectsAddedToCollectionProperties();
                }
                return _objectsAddedToCollections;
            }
        }
    
        #region MethodsForChangeTrackingOnClient
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            _isDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            _isDeserializing = false;
        }
    
        // 将 ObjectChangeTracker 重置为 Unchanged 状态，并
        // 清除原始值和
        // 集合属性的更改记录
        public void AcceptChanges()
        {
            OnObjectStateChanging(ObjectState.Unchanged);
            OriginalValues.Clear();
            ObjectsAddedToCollectionProperties.Clear();
            ObjectsRemovedFromCollectionProperties.Clear();
            ChangeTrackingEnabled = true;
            _objectState = ObjectState.Unchanged;
        }
    
        // 捕获正在更改的属性的原始值。
        internal void RecordOriginalValue(string propertyName, object value)
        {
            if (_changeTrackingEnabled && _objectState != ObjectState.Added)
            {
                if (!OriginalValues.ContainsKey(propertyName))
                {
                    OriginalValues[propertyName] = value;
                }
            }
        }
    
        // 记录对 SelfTracking 实体的集合值属性的添加操作。
        internal void RecordAdditionToCollectionProperties(string propertyName, object value)
        {
            if (_changeTrackingEnabled)
            {
                // 如果在删除实体后重新添加该实体，则不应当在此处执行任何操作
                if (ObjectsRemovedFromCollectionProperties.ContainsKey(propertyName)
                    && ObjectsRemovedFromCollectionProperties[propertyName].Contains(value))
                {
                    ObjectsRemovedFromCollectionProperties[propertyName].Remove(value);
                    if (ObjectsRemovedFromCollectionProperties[propertyName].Count == 0)
                    {
                        ObjectsRemovedFromCollectionProperties.Remove(propertyName);
                    }
                    return;
                }
    
                if (!ObjectsAddedToCollectionProperties.ContainsKey(propertyName))
                {
                    ObjectsAddedToCollectionProperties[propertyName] = new ObjectList();
                    ObjectsAddedToCollectionProperties[propertyName].Add(value);
                }
                else
                {
                    ObjectsAddedToCollectionProperties[propertyName].Add(value);
                }
            }
        }
    
        // 记录对 SelfTracking 实体的集合值属性的删除操作。
        internal void RecordRemovalFromCollectionProperties(string propertyName, object value)
        {
            if (_changeTrackingEnabled)
            {
                // 如果在添加实体后重新删除该实体，则不应当在此处执行任何操作
                if (ObjectsAddedToCollectionProperties.ContainsKey(propertyName)
                    && ObjectsAddedToCollectionProperties[propertyName].Contains(value))
                {
                    ObjectsAddedToCollectionProperties[propertyName].Remove(value);
                    if (ObjectsAddedToCollectionProperties[propertyName].Count == 0)
                    {
                        ObjectsAddedToCollectionProperties.Remove(propertyName);
                    }
                    return;
                }
    
                if (!ObjectsRemovedFromCollectionProperties.ContainsKey(propertyName))
                {
                    ObjectsRemovedFromCollectionProperties[propertyName] = new ObjectList();
                    ObjectsRemovedFromCollectionProperties[propertyName].Add(value);
                }
                else
                {
                    if (!ObjectsRemovedFromCollectionProperties[propertyName].Contains(value))
                    {
                        ObjectsRemovedFromCollectionProperties[propertyName].Add(value);
                    }
                }
            }
        }
        #endregion
    }
    
    #region EnumForObjectState
    [Flags]
    public enum ObjectState
    {
        Unchanged = 0x1,
        Added = 0x2,
        Modified = 0x4,
        Deleted = 0x8
    }
    #endregion
    
    [CollectionDataContract (Name = "ObjectsAddedToCollectionProperties",
        ItemName = "AddedObjectsForProperty", KeyName = "CollectionPropertyName", ValueName = "AddedObjects")]
    public class ObjectsAddedToCollectionProperties : Dictionary<string, ObjectList> { }
    
    [CollectionDataContract (Name = "ObjectsRemovedFromCollectionProperties",
        ItemName = "DeletedObjectsForProperty", KeyName = "CollectionPropertyName",ValueName = "DeletedObjects")]
    public class ObjectsRemovedFromCollectionProperties : Dictionary<string, ObjectList> { }
    
    [CollectionDataContract(Name = "OriginalValuesDictionary",
        ItemName = "OriginalValues", KeyName = "Name", ValueName = "OriginalValue")]
    public class OriginalValuesDictionary : Dictionary<string, Object> { }
    
    [CollectionDataContract(Name = "ExtendedPropertiesDictionary",
        ItemName = "ExtendedProperties", KeyName = "Name", ValueName = "ExtendedProperty")]
    public class ExtendedPropertiesDictionary : Dictionary<string, Object> { }
    
    [CollectionDataContract(ItemName = "ObjectValue")]
    public class ObjectList : List<object> { }
    // 接口由 EF 将生成的自跟踪实体实现。
    // 我们将提供一个将此接口转换为 EF 的所需接口的适配器。
    // 该适配器将在服务器端运行。
    public interface IObjectWithChangeTracker
    {
        //具有给定对象的子图的所有更改跟踪信息。
        ObjectChangeTracker ChangeTracker { get; }
    }
    
    public class ObjectStateChangingEventArgs : EventArgs
    {
        public ObjectState NewState { get; set; }
    }
    
    public static class ObjectWithChangeTrackerExtensions
    {
        public static T MarkAsDeleted<T>(this T trackingItem) where T : IObjectWithChangeTracker
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
            trackingItem.ChangeTracker.State = ObjectState.Deleted;
            return trackingItem;
        }
    
        public static T MarkAsAdded<T>(this T trackingItem) where T : IObjectWithChangeTracker
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
            trackingItem.ChangeTracker.State = ObjectState.Added;
            return trackingItem;
        }
    
        public static T MarkAsModified<T>(this T trackingItem) where T : IObjectWithChangeTracker
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
            trackingItem.ChangeTracker.State = ObjectState.Modified;
            return trackingItem;
        }
    
        public static T MarkAsUnchanged<T>(this T trackingItem) where T : IObjectWithChangeTracker
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
            trackingItem.ChangeTracker.State = ObjectState.Unchanged;
            return trackingItem;
        }
    
        public static void StartTracking(this IObjectWithChangeTracker trackingItem)
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        public static void StopTracking(this IObjectWithChangeTracker trackingItem)
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.ChangeTrackingEnabled = false;
        }
    
        public static void AcceptChanges(this IObjectWithChangeTracker trackingItem)
        {
            if (trackingItem == null)
            {
                throw new ArgumentNullException("trackingItem");
            }
    
            trackingItem.ChangeTracker.AcceptChanges();
        }
    }
    
    // 在清除时引发各项删除通知
    // 并防止添加重复项的 System.Collections.ObjectModel.ObservableCollection。
    public class TrackableCollection<T> : ObservableCollection<T>
    {
        protected override void ClearItems()
        {
            new List<T>(this).ForEach(t => Remove(t));
        }
    
        protected override void InsertItem(int index, T item)
        {
            if (!this.Contains(item))
            {
                base.InsertItem(index, item);
            }
        }
    }
    
    // 一个提供当复杂属性发生更改时所触发的事件的接口。
    // 更改可以是使用新的复杂类型实例替换复杂属性，也可以是
    // 更改复杂类型实例中的标量属性。
    public interface INotifyComplexPropertyChanging
    {
        event EventHandler ComplexPropertyChanging;
    }
    
    public static class EqualityComparer
    {
        // 用于确定两个字节数组即使是不同对象引用也具有相同值的帮助器方法
        public static bool BinaryEquals(object binaryValue1, object binaryValue2)
        {
            if (Object.ReferenceEquals(binaryValue1, binaryValue2))
            {
                return true;
            }
    
            byte[] array1 = binaryValue1 as byte[];
            byte[] array2 = binaryValue2 as byte[];
    
            if (array1 != null && array2 != null)
            {
                if (array1.Length != array2.Length)
                {
                    return false;
                }
    
                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i] != array2[i])
                    {
                        return false;
                    }
                }
    
                return true;
            }
    
            return false;
        }
    }
}
