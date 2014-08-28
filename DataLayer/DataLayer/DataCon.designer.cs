﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DEVDB")]
	public partial class DataConDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InserttblType(tblType instance);
    partial void UpdatetblType(tblType instance);
    partial void DeletetblType(tblType instance);
    partial void InserttblMachine(tblMachine instance);
    partial void UpdatetblMachine(tblMachine instance);
    partial void DeletetblMachine(tblMachine instance);
    partial void InserttblUser(tblUser instance);
    partial void UpdatetblUser(tblUser instance);
    partial void DeletetblUser(tblUser instance);
    partial void InserttblMonitoring(tblMonitoring instance);
    partial void UpdatetblMonitoring(tblMonitoring instance);
    partial void DeletetblMonitoring(tblMonitoring instance);
    partial void InserttblSuppressedNotification(tblSuppressedNotification instance);
    partial void UpdatetblSuppressedNotification(tblSuppressedNotification instance);
    partial void DeletetblSuppressedNotification(tblSuppressedNotification instance);
    partial void InserttblEventLog(tblEventLog instance);
    partial void UpdatetblEventLog(tblEventLog instance);
    partial void DeletetblEventLog(tblEventLog instance);
    #endregion
		
		public DataConDataContext() : 
				base(global::DataLayer.Properties.Settings.Default.DEVDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataConDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataConDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataConDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataConDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tblType> tblTypes
		{
			get
			{
				return this.GetTable<tblType>();
			}
		}
		
		public System.Data.Linq.Table<tblMachine> tblMachines
		{
			get
			{
				return this.GetTable<tblMachine>();
			}
		}
		
		public System.Data.Linq.Table<tblUser> tblUsers
		{
			get
			{
				return this.GetTable<tblUser>();
			}
		}
		
		public System.Data.Linq.Table<tblMonitoring> tblMonitorings
		{
			get
			{
				return this.GetTable<tblMonitoring>();
			}
		}
		
		public System.Data.Linq.Table<tblSuppressedNotification> tblSuppressedNotifications
		{
			get
			{
				return this.GetTable<tblSuppressedNotification>();
			}
		}
		
		public System.Data.Linq.Table<tblEventLog> tblEventLogs
		{
			get
			{
				return this.GetTable<tblEventLog>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RetrieveSuppressedList")]
		public ISingleResult<tblSuppressedNotification> RetrieveSuppressedList([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(255)")] string chUsername)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), chUsername);
			return ((ISingleResult<tblSuppressedNotification>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.RetrieveUserEmail")]
		public ISingleResult<tblUser> RetrieveUserEmail([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Char(50)")] string chMachineName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="BigInt")] System.Nullable<long> iEventid)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), chMachineName, iEventid);
			return ((ISingleResult<tblUser>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblType")]
	public partial class tblType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _iTypeId;
		
		private string _vchType;
		
		private System.DateTime _dtInsertDate;
		
		private EntitySet<tblMachine> _tblMachines;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OniTypeIdChanging(int value);
    partial void OniTypeIdChanged();
    partial void OnvchTypeChanging(string value);
    partial void OnvchTypeChanged();
    partial void OndtInsertDateChanging(System.DateTime value);
    partial void OndtInsertDateChanged();
    #endregion
		
		public tblType()
		{
			this._tblMachines = new EntitySet<tblMachine>(new Action<tblMachine>(this.attach_tblMachines), new Action<tblMachine>(this.detach_tblMachines));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iTypeId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int iTypeId
		{
			get
			{
				return this._iTypeId;
			}
			set
			{
				if ((this._iTypeId != value))
				{
					this.OniTypeIdChanging(value);
					this.SendPropertyChanging();
					this._iTypeId = value;
					this.SendPropertyChanged("iTypeId");
					this.OniTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchType", DbType="VarChar(100)")]
		public string vchType
		{
			get
			{
				return this._vchType;
			}
			set
			{
				if ((this._vchType != value))
				{
					this.OnvchTypeChanging(value);
					this.SendPropertyChanging();
					this._vchType = value;
					this.SendPropertyChanged("vchType");
					this.OnvchTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtInsertDate", DbType="DateTime NOT NULL")]
		public System.DateTime dtInsertDate
		{
			get
			{
				return this._dtInsertDate;
			}
			set
			{
				if ((this._dtInsertDate != value))
				{
					this.OndtInsertDateChanging(value);
					this.SendPropertyChanging();
					this._dtInsertDate = value;
					this.SendPropertyChanged("dtInsertDate");
					this.OndtInsertDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblType_tblMachine", Storage="_tblMachines", ThisKey="iTypeId", OtherKey="iType")]
		public EntitySet<tblMachine> tblMachines
		{
			get
			{
				return this._tblMachines;
			}
			set
			{
				this._tblMachines.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_tblMachines(tblMachine entity)
		{
			this.SendPropertyChanging();
			entity.tblType = this;
		}
		
		private void detach_tblMachines(tblMachine entity)
		{
			this.SendPropertyChanging();
			entity.tblType = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblMachine")]
	public partial class tblMachine : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _chMachineName;
		
		private string _vchEnvironment;
		
		private System.Nullable<int> _iType;
		
		private System.DateTime _dtAdded;
		
		private EntitySet<tblMonitoring> _tblMonitorings;
		
		private EntityRef<tblType> _tblType;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnchMachineNameChanging(string value);
    partial void OnchMachineNameChanged();
    partial void OnvchEnvironmentChanging(string value);
    partial void OnvchEnvironmentChanged();
    partial void OniTypeChanging(System.Nullable<int> value);
    partial void OniTypeChanged();
    partial void OndtAddedChanging(System.DateTime value);
    partial void OndtAddedChanged();
    #endregion
		
		public tblMachine()
		{
			this._tblMonitorings = new EntitySet<tblMonitoring>(new Action<tblMonitoring>(this.attach_tblMonitorings), new Action<tblMonitoring>(this.detach_tblMonitorings));
			this._tblType = default(EntityRef<tblType>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chMachineName", DbType="Char(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string chMachineName
		{
			get
			{
				return this._chMachineName;
			}
			set
			{
				if ((this._chMachineName != value))
				{
					this.OnchMachineNameChanging(value);
					this.SendPropertyChanging();
					this._chMachineName = value;
					this.SendPropertyChanged("chMachineName");
					this.OnchMachineNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchEnvironment", DbType="VarChar(150) NOT NULL", CanBeNull=false)]
		public string vchEnvironment
		{
			get
			{
				return this._vchEnvironment;
			}
			set
			{
				if ((this._vchEnvironment != value))
				{
					this.OnvchEnvironmentChanging(value);
					this.SendPropertyChanging();
					this._vchEnvironment = value;
					this.SendPropertyChanged("vchEnvironment");
					this.OnvchEnvironmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iType", DbType="Int NOT NULL")]
		public System.Nullable<int> iType
		{
			get
			{
				return this._iType;
			}
			set
			{
				if ((this._iType != value))
				{
					if (this._tblType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OniTypeChanging(value);
					this.SendPropertyChanging();
					this._iType = value;
					this.SendPropertyChanged("iType");
					this.OniTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtAdded", DbType="DateTime NOT NULL")]
		public System.DateTime dtAdded
		{
			get
			{
				return this._dtAdded;
			}
			set
			{
				if ((this._dtAdded != value))
				{
					this.OndtAddedChanging(value);
					this.SendPropertyChanging();
					this._dtAdded = value;
					this.SendPropertyChanged("dtAdded");
					this.OndtAddedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblMachine_tblMonitoring", Storage="_tblMonitorings", ThisKey="chMachineName", OtherKey="chUsername")]
		public EntitySet<tblMonitoring> tblMonitorings
		{
			get
			{
				return this._tblMonitorings;
			}
			set
			{
				this._tblMonitorings.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblType_tblMachine", Storage="_tblType", ThisKey="iType", OtherKey="iTypeId", IsForeignKey=true)]
		public tblType tblType
		{
			get
			{
				return this._tblType.Entity;
			}
			set
			{
				tblType previousValue = this._tblType.Entity;
				if (((previousValue != value) 
							|| (this._tblType.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tblType.Entity = null;
						previousValue.tblMachines.Remove(this);
					}
					this._tblType.Entity = value;
					if ((value != null))
					{
						value.tblMachines.Add(this);
						this._iType = value.iTypeId;
					}
					else
					{
						this._iType = default(Nullable<int>);
					}
					this.SendPropertyChanged("tblType");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_tblMonitorings(tblMonitoring entity)
		{
			this.SendPropertyChanging();
			entity.tblMachine = this;
		}
		
		private void detach_tblMonitorings(tblMonitoring entity)
		{
			this.SendPropertyChanging();
			entity.tblMachine = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblUser")]
	public partial class tblUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _chUsername;
		
		private string _vchFirstname;
		
		private string _vchLastname;
		
		private string _vchPrimaryEmail;
		
		private string _vchSecondaryEmail;
		
		private System.Nullable<System.DateTime> _dtInsertdate;
		
		private EntitySet<tblMonitoring> _tblMonitorings;
		
		private EntitySet<tblSuppressedNotification> _tblSuppressedNotifications;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnchUsernameChanging(string value);
    partial void OnchUsernameChanged();
    partial void OnvchFirstnameChanging(string value);
    partial void OnvchFirstnameChanged();
    partial void OnvchLastnameChanging(string value);
    partial void OnvchLastnameChanged();
    partial void OnvchPrimaryEmailChanging(string value);
    partial void OnvchPrimaryEmailChanged();
    partial void OnvchSecondaryEmailChanging(string value);
    partial void OnvchSecondaryEmailChanged();
    partial void OndtInsertdateChanging(System.Nullable<System.DateTime> value);
    partial void OndtInsertdateChanged();
    #endregion
		
		public tblUser()
		{
			this._tblMonitorings = new EntitySet<tblMonitoring>(new Action<tblMonitoring>(this.attach_tblMonitorings), new Action<tblMonitoring>(this.detach_tblMonitorings));
			this._tblSuppressedNotifications = new EntitySet<tblSuppressedNotification>(new Action<tblSuppressedNotification>(this.attach_tblSuppressedNotifications), new Action<tblSuppressedNotification>(this.detach_tblSuppressedNotifications));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chUsername", DbType="NChar(80) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string chUsername
		{
			get
			{
				return this._chUsername;
			}
			set
			{
				if ((this._chUsername != value))
				{
					this.OnchUsernameChanging(value);
					this.SendPropertyChanging();
					this._chUsername = value;
					this.SendPropertyChanged("chUsername");
					this.OnchUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchFirstname", DbType="VarChar(255)")]
		public string vchFirstname
		{
			get
			{
				return this._vchFirstname;
			}
			set
			{
				if ((this._vchFirstname != value))
				{
					this.OnvchFirstnameChanging(value);
					this.SendPropertyChanging();
					this._vchFirstname = value;
					this.SendPropertyChanged("vchFirstname");
					this.OnvchFirstnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchLastname", DbType="VarChar(255)")]
		public string vchLastname
		{
			get
			{
				return this._vchLastname;
			}
			set
			{
				if ((this._vchLastname != value))
				{
					this.OnvchLastnameChanging(value);
					this.SendPropertyChanging();
					this._vchLastname = value;
					this.SendPropertyChanged("vchLastname");
					this.OnvchLastnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchPrimaryEmail", DbType="VarChar(255)")]
		public string vchPrimaryEmail
		{
			get
			{
				return this._vchPrimaryEmail;
			}
			set
			{
				if ((this._vchPrimaryEmail != value))
				{
					this.OnvchPrimaryEmailChanging(value);
					this.SendPropertyChanging();
					this._vchPrimaryEmail = value;
					this.SendPropertyChanged("vchPrimaryEmail");
					this.OnvchPrimaryEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchSecondaryEmail", DbType="VarChar(255)")]
		public string vchSecondaryEmail
		{
			get
			{
				return this._vchSecondaryEmail;
			}
			set
			{
				if ((this._vchSecondaryEmail != value))
				{
					this.OnvchSecondaryEmailChanging(value);
					this.SendPropertyChanging();
					this._vchSecondaryEmail = value;
					this.SendPropertyChanged("vchSecondaryEmail");
					this.OnvchSecondaryEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtInsertdate", DbType="DateTime")]
		public System.Nullable<System.DateTime> dtInsertdate
		{
			get
			{
				return this._dtInsertdate;
			}
			set
			{
				if ((this._dtInsertdate != value))
				{
					this.OndtInsertdateChanging(value);
					this.SendPropertyChanging();
					this._dtInsertdate = value;
					this.SendPropertyChanged("dtInsertdate");
					this.OndtInsertdateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblUser_tblMonitoring", Storage="_tblMonitorings", ThisKey="chUsername", OtherKey="chUsername")]
		public EntitySet<tblMonitoring> tblMonitorings
		{
			get
			{
				return this._tblMonitorings;
			}
			set
			{
				this._tblMonitorings.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblUser_tblSuppressedNotification", Storage="_tblSuppressedNotifications", ThisKey="chUsername", OtherKey="chUsername")]
		public EntitySet<tblSuppressedNotification> tblSuppressedNotifications
		{
			get
			{
				return this._tblSuppressedNotifications;
			}
			set
			{
				this._tblSuppressedNotifications.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_tblMonitorings(tblMonitoring entity)
		{
			this.SendPropertyChanging();
			entity.tblUser = this;
		}
		
		private void detach_tblMonitorings(tblMonitoring entity)
		{
			this.SendPropertyChanging();
			entity.tblUser = null;
		}
		
		private void attach_tblSuppressedNotifications(tblSuppressedNotification entity)
		{
			this.SendPropertyChanging();
			entity.tblUser = this;
		}
		
		private void detach_tblSuppressedNotifications(tblSuppressedNotification entity)
		{
			this.SendPropertyChanging();
			entity.tblUser = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblMonitoring")]
	public partial class tblMonitoring : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _iMonitoringId;
		
		private string _chUsername;
		
		private string _chMachineName;
		
		private System.DateTime _dtInserted;
		
		private EntityRef<tblUser> _tblUser;
		
		private EntityRef<tblMachine> _tblMachine;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OniMonitoringIdChanging(int value);
    partial void OniMonitoringIdChanged();
    partial void OnchUsernameChanging(string value);
    partial void OnchUsernameChanged();
    partial void OnchMachineNameChanging(string value);
    partial void OnchMachineNameChanged();
    partial void OndtInsertedChanging(System.DateTime value);
    partial void OndtInsertedChanged();
    #endregion
		
		public tblMonitoring()
		{
			this._tblUser = default(EntityRef<tblUser>);
			this._tblMachine = default(EntityRef<tblMachine>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iMonitoringId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int iMonitoringId
		{
			get
			{
				return this._iMonitoringId;
			}
			set
			{
				if ((this._iMonitoringId != value))
				{
					this.OniMonitoringIdChanging(value);
					this.SendPropertyChanging();
					this._iMonitoringId = value;
					this.SendPropertyChanged("iMonitoringId");
					this.OniMonitoringIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chUsername", DbType="Char(80) NOT NULL", CanBeNull=false)]
		public string chUsername
		{
			get
			{
				return this._chUsername;
			}
			set
			{
				if ((this._chUsername != value))
				{
					if ((this._tblUser.HasLoadedOrAssignedValue || this._tblMachine.HasLoadedOrAssignedValue))
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnchUsernameChanging(value);
					this.SendPropertyChanging();
					this._chUsername = value;
					this.SendPropertyChanged("chUsername");
					this.OnchUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chMachineName", DbType="Char(50) NOT NULL", CanBeNull=false)]
		public string chMachineName
		{
			get
			{
				return this._chMachineName;
			}
			set
			{
				if ((this._chMachineName != value))
				{
					this.OnchMachineNameChanging(value);
					this.SendPropertyChanging();
					this._chMachineName = value;
					this.SendPropertyChanged("chMachineName");
					this.OnchMachineNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtInserted", DbType="DateTime NOT NULL")]
		public System.DateTime dtInserted
		{
			get
			{
				return this._dtInserted;
			}
			set
			{
				if ((this._dtInserted != value))
				{
					this.OndtInsertedChanging(value);
					this.SendPropertyChanging();
					this._dtInserted = value;
					this.SendPropertyChanged("dtInserted");
					this.OndtInsertedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblUser_tblMonitoring", Storage="_tblUser", ThisKey="chUsername", OtherKey="chUsername", IsForeignKey=true)]
		public tblUser tblUser
		{
			get
			{
				return this._tblUser.Entity;
			}
			set
			{
				tblUser previousValue = this._tblUser.Entity;
				if (((previousValue != value) 
							|| (this._tblUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tblUser.Entity = null;
						previousValue.tblMonitorings.Remove(this);
					}
					this._tblUser.Entity = value;
					if ((value != null))
					{
						value.tblMonitorings.Add(this);
						this._chUsername = value.chUsername;
					}
					else
					{
						this._chUsername = default(string);
					}
					this.SendPropertyChanged("tblUser");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblMachine_tblMonitoring", Storage="_tblMachine", ThisKey="chUsername", OtherKey="chMachineName", IsForeignKey=true)]
		public tblMachine tblMachine
		{
			get
			{
				return this._tblMachine.Entity;
			}
			set
			{
				tblMachine previousValue = this._tblMachine.Entity;
				if (((previousValue != value) 
							|| (this._tblMachine.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tblMachine.Entity = null;
						previousValue.tblMonitorings.Remove(this);
					}
					this._tblMachine.Entity = value;
					if ((value != null))
					{
						value.tblMonitorings.Add(this);
						this._chUsername = value.chMachineName;
					}
					else
					{
						this._chUsername = default(string);
					}
					this.SendPropertyChanged("tblMachine");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblSuppressedNotification")]
	public partial class tblSuppressedNotification : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _iSuppressedId;
		
		private string _chUsername;
		
		private long _iEventId;
		
		private string _vchSource;
		
		private EntityRef<tblUser> _tblUser;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OniSuppressedIdChanging(int value);
    partial void OniSuppressedIdChanged();
    partial void OnchUsernameChanging(string value);
    partial void OnchUsernameChanged();
    partial void OniEventIdChanging(long value);
    partial void OniEventIdChanged();
    partial void OnvchSourceChanging(string value);
    partial void OnvchSourceChanged();
    #endregion
		
		public tblSuppressedNotification()
		{
			this._tblUser = default(EntityRef<tblUser>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iSuppressedId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int iSuppressedId
		{
			get
			{
				return this._iSuppressedId;
			}
			set
			{
				if ((this._iSuppressedId != value))
				{
					this.OniSuppressedIdChanging(value);
					this.SendPropertyChanging();
					this._iSuppressedId = value;
					this.SendPropertyChanged("iSuppressedId");
					this.OniSuppressedIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chUsername", DbType="NChar(80) NOT NULL", CanBeNull=false)]
		public string chUsername
		{
			get
			{
				return this._chUsername;
			}
			set
			{
				if ((this._chUsername != value))
				{
					if (this._tblUser.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnchUsernameChanging(value);
					this.SendPropertyChanging();
					this._chUsername = value;
					this.SendPropertyChanged("chUsername");
					this.OnchUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iEventId", DbType="BigInt NOT NULL")]
		public long iEventId
		{
			get
			{
				return this._iEventId;
			}
			set
			{
				if ((this._iEventId != value))
				{
					this.OniEventIdChanging(value);
					this.SendPropertyChanging();
					this._iEventId = value;
					this.SendPropertyChanged("iEventId");
					this.OniEventIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchSource", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string vchSource
		{
			get
			{
				return this._vchSource;
			}
			set
			{
				if ((this._vchSource != value))
				{
					this.OnvchSourceChanging(value);
					this.SendPropertyChanging();
					this._vchSource = value;
					this.SendPropertyChanged("vchSource");
					this.OnvchSourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tblUser_tblSuppressedNotification", Storage="_tblUser", ThisKey="chUsername", OtherKey="chUsername", IsForeignKey=true)]
		public tblUser tblUser
		{
			get
			{
				return this._tblUser.Entity;
			}
			set
			{
				tblUser previousValue = this._tblUser.Entity;
				if (((previousValue != value) 
							|| (this._tblUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tblUser.Entity = null;
						previousValue.tblSuppressedNotifications.Remove(this);
					}
					this._tblUser.Entity = value;
					if ((value != null))
					{
						value.tblSuppressedNotifications.Add(this);
						this._chUsername = value.chUsername;
					}
					else
					{
						this._chUsername = default(string);
					}
					this.SendPropertyChanged("tblUser");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblEventLog")]
	public partial class tblEventLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _GUID;
		
		private System.Nullable<long> _iEventId;
		
		private string _vchEventMessage;
		
		private string _vchEventType;
		
		private string _chMachineName;
		
		private string _chUsername;
		
		private string _vchSource;
		
		private System.Nullable<System.DateTime> _dtTimeGenerated;
		
		private string _vchComment;
		
		private System.Nullable<byte> _bIsResolved;
		
		private string _chResolvedBy;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGUIDChanging(System.Guid value);
    partial void OnGUIDChanged();
    partial void OniEventIdChanging(System.Nullable<long> value);
    partial void OniEventIdChanged();
    partial void OnvchEventMessageChanging(string value);
    partial void OnvchEventMessageChanged();
    partial void OnvchEventTypeChanging(string value);
    partial void OnvchEventTypeChanged();
    partial void OnchMachineNameChanging(string value);
    partial void OnchMachineNameChanged();
    partial void OnchUsernameChanging(string value);
    partial void OnchUsernameChanged();
    partial void OnvchSourceChanging(string value);
    partial void OnvchSourceChanged();
    partial void OndtTimeGeneratedChanging(System.Nullable<System.DateTime> value);
    partial void OndtTimeGeneratedChanged();
    partial void OnvchCommentChanging(string value);
    partial void OnvchCommentChanged();
    partial void OnbIsResolvedChanging(System.Nullable<byte> value);
    partial void OnbIsResolvedChanged();
    partial void OnchResolvedByChanging(string value);
    partial void OnchResolvedByChanged();
    #endregion
		
		public tblEventLog()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GUID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true, IsDbGenerated=true)]
		public System.Guid GUID
		{
			get
			{
				return this._GUID;
			}
			set
			{
				if ((this._GUID != value))
				{
					this.OnGUIDChanging(value);
					this.SendPropertyChanging();
					this._GUID = value;
					this.SendPropertyChanged("GUID");
					this.OnGUIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_iEventId", DbType="BigInt")]
		public System.Nullable<long> iEventId
		{
			get
			{
				return this._iEventId;
			}
			set
			{
				if ((this._iEventId != value))
				{
					this.OniEventIdChanging(value);
					this.SendPropertyChanging();
					this._iEventId = value;
					this.SendPropertyChanged("iEventId");
					this.OniEventIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchEventMessage", DbType="VarChar(MAX)")]
		public string vchEventMessage
		{
			get
			{
				return this._vchEventMessage;
			}
			set
			{
				if ((this._vchEventMessage != value))
				{
					this.OnvchEventMessageChanging(value);
					this.SendPropertyChanging();
					this._vchEventMessage = value;
					this.SendPropertyChanged("vchEventMessage");
					this.OnvchEventMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchEventType", DbType="VarChar(255)")]
		public string vchEventType
		{
			get
			{
				return this._vchEventType;
			}
			set
			{
				if ((this._vchEventType != value))
				{
					this.OnvchEventTypeChanging(value);
					this.SendPropertyChanging();
					this._vchEventType = value;
					this.SendPropertyChanged("vchEventType");
					this.OnvchEventTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chMachineName", DbType="Char(50)")]
		public string chMachineName
		{
			get
			{
				return this._chMachineName;
			}
			set
			{
				if ((this._chMachineName != value))
				{
					this.OnchMachineNameChanging(value);
					this.SendPropertyChanging();
					this._chMachineName = value;
					this.SendPropertyChanged("chMachineName");
					this.OnchMachineNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chUsername", DbType="Char(80)")]
		public string chUsername
		{
			get
			{
				return this._chUsername;
			}
			set
			{
				if ((this._chUsername != value))
				{
					this.OnchUsernameChanging(value);
					this.SendPropertyChanging();
					this._chUsername = value;
					this.SendPropertyChanged("chUsername");
					this.OnchUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchSource", DbType="VarChar(200)")]
		public string vchSource
		{
			get
			{
				return this._vchSource;
			}
			set
			{
				if ((this._vchSource != value))
				{
					this.OnvchSourceChanging(value);
					this.SendPropertyChanging();
					this._vchSource = value;
					this.SendPropertyChanged("vchSource");
					this.OnvchSourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtTimeGenerated", DbType="DateTime")]
		public System.Nullable<System.DateTime> dtTimeGenerated
		{
			get
			{
				return this._dtTimeGenerated;
			}
			set
			{
				if ((this._dtTimeGenerated != value))
				{
					this.OndtTimeGeneratedChanging(value);
					this.SendPropertyChanging();
					this._dtTimeGenerated = value;
					this.SendPropertyChanged("dtTimeGenerated");
					this.OndtTimeGeneratedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vchComment", DbType="VarChar(MAX)")]
		public string vchComment
		{
			get
			{
				return this._vchComment;
			}
			set
			{
				if ((this._vchComment != value))
				{
					this.OnvchCommentChanging(value);
					this.SendPropertyChanging();
					this._vchComment = value;
					this.SendPropertyChanged("vchComment");
					this.OnvchCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bIsResolved", DbType="TinyInt")]
		public System.Nullable<byte> bIsResolved
		{
			get
			{
				return this._bIsResolved;
			}
			set
			{
				if ((this._bIsResolved != value))
				{
					this.OnbIsResolvedChanging(value);
					this.SendPropertyChanging();
					this._bIsResolved = value;
					this.SendPropertyChanged("bIsResolved");
					this.OnbIsResolvedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_chResolvedBy", DbType="Char(80)")]
		public string chResolvedBy
		{
			get
			{
				return this._chResolvedBy;
			}
			set
			{
				if ((this._chResolvedBy != value))
				{
					this.OnchResolvedByChanging(value);
					this.SendPropertyChanging();
					this._chResolvedBy = value;
					this.SendPropertyChanged("chResolvedBy");
					this.OnchResolvedByChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
