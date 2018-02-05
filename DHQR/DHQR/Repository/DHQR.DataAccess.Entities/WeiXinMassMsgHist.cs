//------------------------------------------------------------------------------
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
    [DataContract(IsReference = true)]
    public partial class WeiXinMassMsgHist: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region 基元属性
    
        [DataMember]
        public System.Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("属性“Id”是对象键的一部分，不可更改。仅当未跟踪对象或对象处于“已添加”状态时，才能对键属性进行更改。");
                    }
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private System.Guid _id;
    
        [DataMember]
        public string msg_id
        {
            get { return _msg_id; }
            set
            {
                if (_msg_id != value)
                {
                    _msg_id = value;
                    OnPropertyChanged("msg_id");
                }
            }
        }
        private string _msg_id;
    
        [DataMember]
        public string type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("type");
                }
            }
        }
        private string _type;
    
        [DataMember]
        public int tagetid
        {
            get { return _tagetid; }
            set
            {
                if (_tagetid != value)
                {
                    _tagetid = value;
                    OnPropertyChanged("tagetid");
                }
            }
        }
        private int _tagetid;
    
        [DataMember]
        public Nullable<int> groupid
        {
            get { return _groupid; }
            set
            {
                if (_groupid != value)
                {
                    _groupid = value;
                    OnPropertyChanged("groupid");
                }
            }
        }
        private Nullable<int> _groupid;
    
        [DataMember]
        public string content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged("content");
                }
            }
        }
        private string _content;
    
        [DataMember]
        public string msg_status
        {
            get { return _msg_status; }
            set
            {
                if (_msg_status != value)
                {
                    _msg_status = value;
                    OnPropertyChanged("msg_status");
                }
            }
        }
        private string _msg_status;
    
        [DataMember]
        public string msg_desc
        {
            get { return _msg_desc; }
            set
            {
                if (_msg_desc != value)
                {
                    _msg_desc = value;
                    OnPropertyChanged("msg_desc");
                }
            }
        }
        private string _msg_desc;
    
        [DataMember]
        public Nullable<int> TotalCount
        {
            get { return _totalCount; }
            set
            {
                if (_totalCount != value)
                {
                    _totalCount = value;
                    OnPropertyChanged("TotalCount");
                }
            }
        }
        private Nullable<int> _totalCount;
    
        [DataMember]
        public Nullable<int> FilterCount
        {
            get { return _filterCount; }
            set
            {
                if (_filterCount != value)
                {
                    _filterCount = value;
                    OnPropertyChanged("FilterCount");
                }
            }
        }
        private Nullable<int> _filterCount;
    
        [DataMember]
        public Nullable<int> SentCount
        {
            get { return _sentCount; }
            set
            {
                if (_sentCount != value)
                {
                    _sentCount = value;
                    OnPropertyChanged("SentCount");
                }
            }
        }
        private Nullable<int> _sentCount;
    
        [DataMember]
        public Nullable<int> ErrorCount
        {
            get { return _errorCount; }
            set
            {
                if (_errorCount != value)
                {
                    _errorCount = value;
                    OnPropertyChanged("ErrorCount");
                }
            }
        }
        private Nullable<int> _errorCount;
    
        [DataMember]
        public Nullable<System.Guid> WeiXinMassMsgId
        {
            get { return _weiXinMassMsgId; }
            set
            {
                if (_weiXinMassMsgId != value)
                {
                    _weiXinMassMsgId = value;
                    OnPropertyChanged("WeiXinMassMsgId");
                }
            }
        }
        private Nullable<System.Guid> _weiXinMassMsgId;
    
        [DataMember]
        public Nullable<System.Guid> WeiXinMediaId
        {
            get { return _weiXinMediaId; }
            set
            {
                if (_weiXinMediaId != value)
                {
                    _weiXinMediaId = value;
                    OnPropertyChanged("WeiXinMediaId");
                }
            }
        }
        private Nullable<System.Guid> _weiXinMediaId;
    
        [DataMember]
        public System.DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                if (_createTime != value)
                {
                    _createTime = value;
                    OnPropertyChanged("CreateTime");
                }
            }
        }
        private System.DateTime _createTime;

        #endregion

        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
        }

        #endregion

    }
}
