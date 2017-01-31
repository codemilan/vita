﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vita.Entities; 
using Vita.Data.Upgrades;

namespace Vita.Data {
  public class DataSourceEvents {

    public event EventHandler<DbUpgradeEventArgs> DbUpgrading;
    public event EventHandler<DbUpgradeEventArgs> DbUpgraded;
    public event EventHandler<DataSourceEventArgs> DataSourceStatusChanging;
    public event EventHandler<DataSourceAddEventArgs> DataSourceAdd;
    private IDataAccessService _service; 

    public DataSourceEvents(IDataAccessService service) {
      _service = service; 
    }

    internal void OnDataSourceStatusChanging(DataSourceEventArgs args) {
      if(DataSourceStatusChanging != null)
        DataSourceStatusChanging(_service, args);
    }
    internal void OnDbUpgrading(DbUpgradeEventArgs args) {
      if(DbUpgrading != null)
        DbUpgrading(_service, args);
    }
    internal void OnDbUpgraded(DbUpgradeEventArgs args) {
      if(DbUpgraded != null)
        DbUpgraded(_service, args);
    }
    internal void OnDataSourceAdd(DataSourceAddEventArgs args) {
      if(DataSourceAdd != null)
        DataSourceAdd(_service, args);
    }
  }

  public enum DataSourceEventType {
    Connecting,
    HigherVersionDetected,
    DbModelUpdating,
    DbModelUpdated,
    Connected,
    Disconnecting,
  }

  public class DataSourceEventArgs : EventArgs {
    public readonly DataSource DataSource;
    public readonly DataSourceEventType EventType;
    public DataSourceEventArgs(DataSource dataSource, DataSourceEventType eventType) {
      DataSource = dataSource;
      EventType = eventType;
    }
  }

  public class DataSourceAddEventArgs : EventArgs {
    public readonly OperationContext Context; 
    public DataSource NewDataSource;
    public DataSourceAddEventArgs(OperationContext context) {
      Context = context;
    }
  }

  public class DbUpgradeEventArgs : EventArgs {
    public readonly DataSource DataSource;
    public readonly List<DbUpgradeScript> Scripts;
    public readonly DateTime StartedOn;
    public readonly DateTime? CompletedOn;
    public readonly DbUpgradeScript FailedScript;
    public readonly Exception Exception;
    public DbUpgradeEventArgs(DataSource dataSource, DataSourceEventType eventType, List<DbUpgradeScript> scripts, 
                                  DateTime startedOn, DateTime? completedOn,
                                  Exception exception = null, DbUpgradeScript failedScript = null) {
      DataSource = dataSource;
      Scripts = scripts;
      StartedOn = startedOn;
      CompletedOn = completedOn;
      Exception = exception;
      FailedScript = failedScript;
    }
  }



}
