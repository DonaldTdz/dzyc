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
    public partial class DistCust: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public string DIST_NUM
        {
            get { return _dIST_NUM; }
            set
            {
                if (_dIST_NUM != value)
                {
                    _dIST_NUM = value;
                    OnPropertyChanged("DIST_NUM");
                }
            }
        }
        private string _dIST_NUM;
    
        [DataMember]
        public string CO_NUM
        {
            get { return _cO_NUM; }
            set
            {
                if (_cO_NUM != value)
                {
                    _cO_NUM = value;
                    OnPropertyChanged("CO_NUM");
                }
            }
        }
        private string _cO_NUM;
    
        [DataMember]
        public string CUST_ID
        {
            get { return _cUST_ID; }
            set
            {
                if (_cUST_ID != value)
                {
                    _cUST_ID = value;
                    OnPropertyChanged("CUST_ID");
                }
            }
        }
        private string _cUST_ID;
    
        [DataMember]
        public string IS_RECEIVED
        {
            get { return _iS_RECEIVED; }
            set
            {
                if (_iS_RECEIVED != value)
                {
                    _iS_RECEIVED = value;
                    OnPropertyChanged("IS_RECEIVED");
                }
            }
        }
        private string _iS_RECEIVED;
    
        [DataMember]
        public string DIST_SATIS
        {
            get { return _dIST_SATIS; }
            set
            {
                if (_dIST_SATIS != value)
                {
                    _dIST_SATIS = value;
                    OnPropertyChanged("DIST_SATIS");
                }
            }
        }
        private string _dIST_SATIS;
    
        [DataMember]
        public string UNLOAD_REASON
        {
            get { return _uNLOAD_REASON; }
            set
            {
                if (_uNLOAD_REASON != value)
                {
                    _uNLOAD_REASON = value;
                    OnPropertyChanged("UNLOAD_REASON");
                }
            }
        }
        private string _uNLOAD_REASON;
    
        [DataMember]
        public string REC_DATE
        {
            get { return _rEC_DATE; }
            set
            {
                if (_rEC_DATE != value)
                {
                    _rEC_DATE = value;
                    OnPropertyChanged("REC_DATE");
                }
            }
        }
        private string _rEC_DATE;
    
        [DataMember]
        public System.DateTime RecieveDate
        {
            get { return _recieveDate; }
            set
            {
                if (_recieveDate != value)
                {
                    _recieveDate = value;
                    OnPropertyChanged("RecieveDate");
                }
            }
        }
        private System.DateTime _recieveDate;
    
        [DataMember]
        public string REC_ARRIVE_TIME
        {
            get { return _rEC_ARRIVE_TIME; }
            set
            {
                if (_rEC_ARRIVE_TIME != value)
                {
                    _rEC_ARRIVE_TIME = value;
                    OnPropertyChanged("REC_ARRIVE_TIME");
                }
            }
        }
        private string _rEC_ARRIVE_TIME;
    
        [DataMember]
        public string REC_LEAVE_TIME
        {
            get { return _rEC_LEAVE_TIME; }
            set
            {
                if (_rEC_LEAVE_TIME != value)
                {
                    _rEC_LEAVE_TIME = value;
                    OnPropertyChanged("REC_LEAVE_TIME");
                }
            }
        }
        private string _rEC_LEAVE_TIME;
    
        [DataMember]
        public decimal HANDOVER_TIME
        {
            get { return _hANDOVER_TIME; }
            set
            {
                if (_hANDOVER_TIME != value)
                {
                    _hANDOVER_TIME = value;
                    OnPropertyChanged("HANDOVER_TIME");
                }
            }
        }
        private decimal _hANDOVER_TIME;
    
        [DataMember]
        public string NOTSATIS_REASON
        {
            get { return _nOTSATIS_REASON; }
            set
            {
                if (_nOTSATIS_REASON != value)
                {
                    _nOTSATIS_REASON = value;
                    OnPropertyChanged("NOTSATIS_REASON");
                }
            }
        }
        private string _nOTSATIS_REASON;
    
        [DataMember]
        public string UNUSUAL_TYPE
        {
            get { return _uNUSUAL_TYPE; }
            set
            {
                if (_uNUSUAL_TYPE != value)
                {
                    _uNUSUAL_TYPE = value;
                    OnPropertyChanged("UNUSUAL_TYPE");
                }
            }
        }
        private string _uNUSUAL_TYPE;
    
        [DataMember]
        public string EVALUATE_INFO
        {
            get { return _eVALUATE_INFO; }
            set
            {
                if (_eVALUATE_INFO != value)
                {
                    _eVALUATE_INFO = value;
                    OnPropertyChanged("EVALUATE_INFO");
                }
            }
        }
        private string _eVALUATE_INFO;
    
        [DataMember]
        public string SIGN_TYPE
        {
            get { return _sIGN_TYPE; }
            set
            {
                if (_sIGN_TYPE != value)
                {
                    _sIGN_TYPE = value;
                    OnPropertyChanged("SIGN_TYPE");
                }
            }
        }
        private string _sIGN_TYPE;
    
        [DataMember]
        public string EXP_SIGN_REASON
        {
            get { return _eXP_SIGN_REASON; }
            set
            {
                if (_eXP_SIGN_REASON != value)
                {
                    _eXP_SIGN_REASON = value;
                    OnPropertyChanged("EXP_SIGN_REASON");
                }
            }
        }
        private string _eXP_SIGN_REASON;
    
        [DataMember]
        public Nullable<decimal> UNLOAD_LON
        {
            get { return _uNLOAD_LON; }
            set
            {
                if (_uNLOAD_LON != value)
                {
                    _uNLOAD_LON = value;
                    OnPropertyChanged("UNLOAD_LON");
                }
            }
        }
        private Nullable<decimal> _uNLOAD_LON;
    
        [DataMember]
        public Nullable<decimal> UNLOAD_LAT
        {
            get { return _uNLOAD_LAT; }
            set
            {
                if (_uNLOAD_LAT != value)
                {
                    _uNLOAD_LAT = value;
                    OnPropertyChanged("UNLOAD_LAT");
                }
            }
        }
        private Nullable<decimal> _uNLOAD_LAT;
    
        [DataMember]
        public Nullable<decimal> UNLOAD_DISTANCE
        {
            get { return _uNLOAD_DISTANCE; }
            set
            {
                if (_uNLOAD_DISTANCE != value)
                {
                    _uNLOAD_DISTANCE = value;
                    OnPropertyChanged("UNLOAD_DISTANCE");
                }
            }
        }
        private Nullable<decimal> _uNLOAD_DISTANCE;
    
        [DataMember]
        public string EVALUATE_TIME
        {
            get { return _eVALUATE_TIME; }
            set
            {
                if (_eVALUATE_TIME != value)
                {
                    _eVALUATE_TIME = value;
                    OnPropertyChanged("EVALUATE_TIME");
                }
            }
        }
        private string _eVALUATE_TIME;
    
        [DataMember]
        public string DLVMAN_ID
        {
            get { return _dLVMAN_ID; }
            set
            {
                if (_dLVMAN_ID != value)
                {
                    _dLVMAN_ID = value;
                    OnPropertyChanged("DLVMAN_ID");
                }
            }
        }
        private string _dLVMAN_ID;

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
