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
    [KnownType(typeof(User))]
    [KnownType(typeof(WeiXinPicMsgMatser))]
    [KnownType(typeof(WeiXinSource))]
    public partial class WeiXinApp: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public string WeiXinKey
        {
            get { return _weiXinKey; }
            set
            {
                if (_weiXinKey != value)
                {
                    _weiXinKey = value;
                    OnPropertyChanged("WeiXinKey");
                }
            }
        }
        private string _weiXinKey;
    
        [DataMember]
        public string AppId
        {
            get { return _appId; }
            set
            {
                if (_appId != value)
                {
                    _appId = value;
                    OnPropertyChanged("AppId");
                }
            }
        }
        private string _appId;
    
        [DataMember]
        public string AppSecret
        {
            get { return _appSecret; }
            set
            {
                if (_appSecret != value)
                {
                    _appSecret = value;
                    OnPropertyChanged("AppSecret");
                }
            }
        }
        private string _appSecret;
    
        [DataMember]
        public string Url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    OnPropertyChanged("Url");
                }
            }
        }
        private string _url;
    
        [DataMember]
        public string Token
        {
            get { return _token; }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    OnPropertyChanged("Token");
                }
            }
        }
        private string _token;
    
        [DataMember]
        public string OriginalId
        {
            get { return _originalId; }
            set
            {
                if (_originalId != value)
                {
                    _originalId = value;
                    OnPropertyChanged("OriginalId");
                }
            }
        }
        private string _originalId;
    
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
    
        [DataMember]
        public System.Guid UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    ChangeTracker.RecordOriginalValue("UserId", _userId);
                    if (!IsDeserializing)
                    {
                        if (User != null && User.Id != value)
                        {
                            User = null;
                        }
                    }
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private System.Guid _userId;
    
        [DataMember]
        public int WeiXinType
        {
            get { return _weiXinType; }
            set
            {
                if (_weiXinType != value)
                {
                    _weiXinType = value;
                    OnPropertyChanged("WeiXinType");
                }
            }
        }
        private int _weiXinType;
    
        [DataMember]
        public string PicUrl
        {
            get { return _picUrl; }
            set
            {
                if (_picUrl != value)
                {
                    _picUrl = value;
                    OnPropertyChanged("PicUrl");
                }
            }
        }
        private string _picUrl;
    
        [DataMember]
        public string access_token
        {
            get { return _access_token; }
            set
            {
                if (_access_token != value)
                {
                    _access_token = value;
                    OnPropertyChanged("access_token");
                }
            }
        }
        private string _access_token;
    
        [DataMember]
        public Nullable<int> expires_in
        {
            get { return _expires_in; }
            set
            {
                if (_expires_in != value)
                {
                    _expires_in = value;
                    OnPropertyChanged("expires_in");
                }
            }
        }
        private Nullable<int> _expires_in;
    
        [DataMember]
        public Nullable<System.DateTime> next_gettime
        {
            get { return _next_gettime; }
            set
            {
                if (_next_gettime != value)
                {
                    _next_gettime = value;
                    OnPropertyChanged("next_gettime");
                }
            }
        }
        private Nullable<System.DateTime> _next_gettime;

        #endregion

        #region 导航属性
    
        [DataMember]
        public User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                    OnNavigationPropertyChanged("User");
                }
            }
        }
        private User _user;
    
        [DataMember]
        public TrackableCollection<WeiXinPicMsgMatser> WeiXinPicMsgMatsers
        {
            get
            {
                if (_weiXinPicMsgMatsers == null)
                {
                    _weiXinPicMsgMatsers = new TrackableCollection<WeiXinPicMsgMatser>();
                    _weiXinPicMsgMatsers.CollectionChanged += FixupWeiXinPicMsgMatsers;
                }
                return _weiXinPicMsgMatsers;
            }
            set
            {
                if (!ReferenceEquals(_weiXinPicMsgMatsers, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("当启用 ChangeTracking 时，无法设置 FixupChangeTrackingCollection");
                    }
                    if (_weiXinPicMsgMatsers != null)
                    {
                        _weiXinPicMsgMatsers.CollectionChanged -= FixupWeiXinPicMsgMatsers;
                    }
                    _weiXinPicMsgMatsers = value;
                    if (_weiXinPicMsgMatsers != null)
                    {
                        _weiXinPicMsgMatsers.CollectionChanged += FixupWeiXinPicMsgMatsers;
                    }
                    OnNavigationPropertyChanged("WeiXinPicMsgMatsers");
                }
            }
        }
        private TrackableCollection<WeiXinPicMsgMatser> _weiXinPicMsgMatsers;
    
        [DataMember]
        public TrackableCollection<WeiXinSource> WeiXinSources
        {
            get
            {
                if (_weiXinSources == null)
                {
                    _weiXinSources = new TrackableCollection<WeiXinSource>();
                    _weiXinSources.CollectionChanged += FixupWeiXinSources;
                }
                return _weiXinSources;
            }
            set
            {
                if (!ReferenceEquals(_weiXinSources, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("当启用 ChangeTracking 时，无法设置 FixupChangeTrackingCollection");
                    }
                    if (_weiXinSources != null)
                    {
                        _weiXinSources.CollectionChanged -= FixupWeiXinSources;
                    }
                    _weiXinSources = value;
                    if (_weiXinSources != null)
                    {
                        _weiXinSources.CollectionChanged += FixupWeiXinSources;
                    }
                    OnNavigationPropertyChanged("WeiXinSources");
                }
            }
        }
        private TrackableCollection<WeiXinSource> _weiXinSources;

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
            User = null;
            WeiXinPicMsgMatsers.Clear();
            WeiXinSources.Clear();
        }

        #endregion

        #region 关联修复
    
        private void FixupUser(User previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.WeiXinApps.Contains(this))
            {
                previousValue.WeiXinApps.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.WeiXinApps.Contains(this))
                {
                    User.WeiXinApps.Add(this);
                }
    
                UserId = User.Id;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("User")
                    && (ChangeTracker.OriginalValues["User"] == User))
                {
                    ChangeTracker.OriginalValues.Remove("User");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("User", previousValue);
                }
                if (User != null && !User.ChangeTracker.ChangeTrackingEnabled)
                {
                    User.StartTracking();
                }
            }
        }
    
        private void FixupWeiXinPicMsgMatsers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (WeiXinPicMsgMatser item in e.NewItems)
                {
                    item.WeiXinApp = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("WeiXinPicMsgMatsers", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (WeiXinPicMsgMatser item in e.OldItems)
                {
                    if (ReferenceEquals(item.WeiXinApp, this))
                    {
                        item.WeiXinApp = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("WeiXinPicMsgMatsers", item);
                    }
                }
            }
        }
    
        private void FixupWeiXinSources(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (WeiXinSource item in e.NewItems)
                {
                    item.WeiXinApp = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("WeiXinSources", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (WeiXinSource item in e.OldItems)
                {
                    if (ReferenceEquals(item.WeiXinApp, this))
                    {
                        item.WeiXinApp = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("WeiXinSources", item);
                    }
                }
            }
        }

        #endregion

    }
}
