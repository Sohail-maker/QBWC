﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuickbooksWebService.DomainModel
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ContentManager")]
	public partial class ContentManagerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertshops_taxRule(shops_taxRule instance);
    partial void Updateshops_taxRule(shops_taxRule instance);
    partial void Deleteshops_taxRule(shops_taxRule instance);
    partial void Insertshops_inventory(shops_inventory instance);
    partial void Updateshops_inventory(shops_inventory instance);
    partial void Deleteshops_inventory(shops_inventory instance);
    #endregion
		
		public ContentManagerDataContext() : 
				base(global::QuickbooksWebService.DomainModel.Properties.Settings.Default.ContentManagerConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public ContentManagerDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentManagerDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentManagerDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContentManagerDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<shops_taxRule> shops_taxRules
		{
			get
			{
				return this.GetTable<shops_taxRule>();
			}
		}
		
		public System.Data.Linq.Table<shops_inventory> shops_inventories
		{
			get
			{
				return this.GetTable<shops_inventory>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.shops_taxRules")]
	public partial class shops_taxRule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _shopID;
		
		private int _clientID;
		
		private string _state;
		
		private decimal _rate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnshopIDChanging(int value);
    partial void OnshopIDChanged();
    partial void OnclientIDChanging(int value);
    partial void OnclientIDChanged();
    partial void OnstateChanging(string value);
    partial void OnstateChanged();
    partial void OnrateChanging(decimal value);
    partial void OnrateChanged();
    #endregion
		
		public shops_taxRule()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shopID", DbType="Int NOT NULL")]
		public int shopID
		{
			get
			{
				return this._shopID;
			}
			set
			{
				if ((this._shopID != value))
				{
					this.OnshopIDChanging(value);
					this.SendPropertyChanging();
					this._shopID = value;
					this.SendPropertyChanged("shopID");
					this.OnshopIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientID", DbType="Int NOT NULL")]
		public int clientID
		{
			get
			{
				return this._clientID;
			}
			set
			{
				if ((this._clientID != value))
				{
					this.OnclientIDChanging(value);
					this.SendPropertyChanging();
					this._clientID = value;
					this.SendPropertyChanged("clientID");
					this.OnclientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_state", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
		public string state
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this.OnstateChanging(value);
					this.SendPropertyChanging();
					this._state = value;
					this.SendPropertyChanged("state");
					this.OnstateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rate", DbType="Decimal(18,3) NOT NULL")]
		public decimal rate
		{
			get
			{
				return this._rate;
			}
			set
			{
				if ((this._rate != value))
				{
					this.OnrateChanging(value);
					this.SendPropertyChanging();
					this._rate = value;
					this.SendPropertyChanged("rate");
					this.OnrateChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.shops_inventory")]
	public partial class shops_inventory : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _clientID;
		
		private int _shopID;
		
		private string _title;
		
		private System.Nullable<decimal> _price1;
		
		private System.Nullable<decimal> _price2;
		
		private System.Nullable<decimal> _price3;
		
		private System.Nullable<int> _weightInOunces;
		
		private System.Nullable<bool> _status;
		
		private System.Nullable<int> _recordID;
		
		private System.Nullable<int> _soldOut;
		
		private System.Nullable<int> _featured;
		
		private System.Nullable<int> _qty;
		
		private System.Nullable<int> _locationId;
		
		private string _tag;
		
		private System.Nullable<bool> _freeShipping;
		
		private System.Nullable<bool> _variablePricing;
		
		private System.Nullable<decimal> _volume_price;
		
		private System.Nullable<int> _volume_qtyRequired;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnclientIDChanging(int value);
    partial void OnclientIDChanged();
    partial void OnshopIDChanging(int value);
    partial void OnshopIDChanged();
    partial void OntitleChanging(string value);
    partial void OntitleChanged();
    partial void Onprice1Changing(System.Nullable<decimal> value);
    partial void Onprice1Changed();
    partial void Onprice2Changing(System.Nullable<decimal> value);
    partial void Onprice2Changed();
    partial void Onprice3Changing(System.Nullable<decimal> value);
    partial void Onprice3Changed();
    partial void OnweightInOuncesChanging(System.Nullable<int> value);
    partial void OnweightInOuncesChanged();
    partial void OnstatusChanging(System.Nullable<bool> value);
    partial void OnstatusChanged();
    partial void OnrecordIDChanging(System.Nullable<int> value);
    partial void OnrecordIDChanged();
    partial void OnsoldOutChanging(System.Nullable<int> value);
    partial void OnsoldOutChanged();
    partial void OnfeaturedChanging(System.Nullable<int> value);
    partial void OnfeaturedChanged();
    partial void OnqtyChanging(System.Nullable<int> value);
    partial void OnqtyChanged();
    partial void OnlocationIdChanging(System.Nullable<int> value);
    partial void OnlocationIdChanged();
    partial void OntagChanging(string value);
    partial void OntagChanged();
    partial void OnfreeShippingChanging(System.Nullable<bool> value);
    partial void OnfreeShippingChanged();
    partial void OnvariablePricingChanging(System.Nullable<bool> value);
    partial void OnvariablePricingChanged();
    partial void Onvolume_priceChanging(System.Nullable<decimal> value);
    partial void Onvolume_priceChanged();
    partial void Onvolume_qtyRequiredChanging(System.Nullable<int> value);
    partial void Onvolume_qtyRequiredChanged();
    #endregion
		
		public shops_inventory()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_clientID", DbType="Int NOT NULL")]
		public int clientID
		{
			get
			{
				return this._clientID;
			}
			set
			{
				if ((this._clientID != value))
				{
					this.OnclientIDChanging(value);
					this.SendPropertyChanging();
					this._clientID = value;
					this.SendPropertyChanged("clientID");
					this.OnclientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shopID", DbType="Int NOT NULL")]
		public int shopID
		{
			get
			{
				return this._shopID;
			}
			set
			{
				if ((this._shopID != value))
				{
					this.OnshopIDChanging(value);
					this.SendPropertyChanging();
					this._shopID = value;
					this.SendPropertyChanged("shopID");
					this.OnshopIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_title", DbType="VarChar(255)")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				if ((this._title != value))
				{
					this.OntitleChanging(value);
					this.SendPropertyChanging();
					this._title = value;
					this.SendPropertyChanged("title");
					this.OntitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_price1", DbType="Money")]
		public System.Nullable<decimal> price1
		{
			get
			{
				return this._price1;
			}
			set
			{
				if ((this._price1 != value))
				{
					this.Onprice1Changing(value);
					this.SendPropertyChanging();
					this._price1 = value;
					this.SendPropertyChanged("price1");
					this.Onprice1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_price2", DbType="Money")]
		public System.Nullable<decimal> price2
		{
			get
			{
				return this._price2;
			}
			set
			{
				if ((this._price2 != value))
				{
					this.Onprice2Changing(value);
					this.SendPropertyChanging();
					this._price2 = value;
					this.SendPropertyChanged("price2");
					this.Onprice2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_price3", DbType="Money")]
		public System.Nullable<decimal> price3
		{
			get
			{
				return this._price3;
			}
			set
			{
				if ((this._price3 != value))
				{
					this.Onprice3Changing(value);
					this.SendPropertyChanging();
					this._price3 = value;
					this.SendPropertyChanged("price3");
					this.Onprice3Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_weightInOunces", DbType="Int")]
		public System.Nullable<int> weightInOunces
		{
			get
			{
				return this._weightInOunces;
			}
			set
			{
				if ((this._weightInOunces != value))
				{
					this.OnweightInOuncesChanging(value);
					this.SendPropertyChanging();
					this._weightInOunces = value;
					this.SendPropertyChanged("weightInOunces");
					this.OnweightInOuncesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status", DbType="Bit")]
		public System.Nullable<bool> status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this.OnstatusChanging(value);
					this.SendPropertyChanging();
					this._status = value;
					this.SendPropertyChanged("status");
					this.OnstatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_recordID", DbType="Int")]
		public System.Nullable<int> recordID
		{
			get
			{
				return this._recordID;
			}
			set
			{
				if ((this._recordID != value))
				{
					this.OnrecordIDChanging(value);
					this.SendPropertyChanging();
					this._recordID = value;
					this.SendPropertyChanged("recordID");
					this.OnrecordIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_soldOut", DbType="Int")]
		public System.Nullable<int> soldOut
		{
			get
			{
				return this._soldOut;
			}
			set
			{
				if ((this._soldOut != value))
				{
					this.OnsoldOutChanging(value);
					this.SendPropertyChanging();
					this._soldOut = value;
					this.SendPropertyChanged("soldOut");
					this.OnsoldOutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_featured", DbType="Int")]
		public System.Nullable<int> featured
		{
			get
			{
				return this._featured;
			}
			set
			{
				if ((this._featured != value))
				{
					this.OnfeaturedChanging(value);
					this.SendPropertyChanging();
					this._featured = value;
					this.SendPropertyChanged("featured");
					this.OnfeaturedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_qty", DbType="Int")]
		public System.Nullable<int> qty
		{
			get
			{
				return this._qty;
			}
			set
			{
				if ((this._qty != value))
				{
					this.OnqtyChanging(value);
					this.SendPropertyChanging();
					this._qty = value;
					this.SendPropertyChanged("qty");
					this.OnqtyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_locationId", DbType="Int")]
		public System.Nullable<int> locationId
		{
			get
			{
				return this._locationId;
			}
			set
			{
				if ((this._locationId != value))
				{
					this.OnlocationIdChanging(value);
					this.SendPropertyChanging();
					this._locationId = value;
					this.SendPropertyChanged("locationId");
					this.OnlocationIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tag", DbType="VarChar(255)")]
		public string tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				if ((this._tag != value))
				{
					this.OntagChanging(value);
					this.SendPropertyChanging();
					this._tag = value;
					this.SendPropertyChanged("tag");
					this.OntagChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_freeShipping", DbType="Bit")]
		public System.Nullable<bool> freeShipping
		{
			get
			{
				return this._freeShipping;
			}
			set
			{
				if ((this._freeShipping != value))
				{
					this.OnfreeShippingChanging(value);
					this.SendPropertyChanging();
					this._freeShipping = value;
					this.SendPropertyChanged("freeShipping");
					this.OnfreeShippingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_variablePricing", DbType="Bit")]
		public System.Nullable<bool> variablePricing
		{
			get
			{
				return this._variablePricing;
			}
			set
			{
				if ((this._variablePricing != value))
				{
					this.OnvariablePricingChanging(value);
					this.SendPropertyChanging();
					this._variablePricing = value;
					this.SendPropertyChanged("variablePricing");
					this.OnvariablePricingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_volume_price", DbType="Money")]
		public System.Nullable<decimal> volume_price
		{
			get
			{
				return this._volume_price;
			}
			set
			{
				if ((this._volume_price != value))
				{
					this.Onvolume_priceChanging(value);
					this.SendPropertyChanging();
					this._volume_price = value;
					this.SendPropertyChanged("volume_price");
					this.Onvolume_priceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_volume_qtyRequired", DbType="Int")]
		public System.Nullable<int> volume_qtyRequired
		{
			get
			{
				return this._volume_qtyRequired;
			}
			set
			{
				if ((this._volume_qtyRequired != value))
				{
					this.Onvolume_qtyRequiredChanging(value);
					this.SendPropertyChanging();
					this._volume_qtyRequired = value;
					this.SendPropertyChanged("volume_qtyRequired");
					this.Onvolume_qtyRequiredChanged();
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
