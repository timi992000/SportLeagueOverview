using SportLeagueOverview.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace SportLeagueOverview.Core
{
  public class ViewModelBase<T> : INotifyPropertyChanged
  {
    private T m_CurrentItem;
    private T m_OriginalCurrentItem;
    private bool m_HasChanges;

    public ViewModelBase(object ViewModel = null)
    {
      __DoReload();
      __InitializeCommands();
    }

    private void __InitializeCommands()
    {
      Save = new DelegateCommand<object>(Execute_Save);
      Delete = new DelegateCommand<object>(Execute_Delete);
      Reload = new DelegateCommand<object>(Execute_Reload);
      Cancel = new DelegateCommand<object>(Execute_Cancel);
    }

    #region [Commands]
    public ICommand Save { get; set; }
    public ICommand Delete { get; set; }
    public ICommand Reload { get; set; }
    public ICommand Cancel { get; set; }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        HasChanges = true;
      }
    }

    public void OnCurrentItemChanged()
    {
      foreach (var Property in this.GetType().GetProperties())
      {
        OnPropertyChanged(Property.Name);
      }
    }
    public List<T> CurrentItems
    {
      get;
      set;
    }

    public T CurrentItem
    {
      get
      {
        return m_CurrentItem ?? Activator.CreateInstance<T>();
      }
      set
      {
        m_CurrentItem = value;
        m_OriginalCurrentItem = (T)(CurrentItem as EntityBase).Clone();
        OnPropertyChanged();
        OnCurrentItemChanged();
        HasChanges = false;
      }
    }

    public bool HasChanges
    {
      get
      {
        return m_HasChanges;
      }
      set
      {
        m_HasChanges = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasChanges)));
      }
    }

    public Brush HasChangesBrush => HasChanges ? Brushes.Red : Brushes.Black;

    public void Execute_Save(object sender)
    {

    }

    public void Execute_Delete(object sender)
    {
      var Result = DatenbankHelfer.DeleteEntity(CurrentItem);
      if (Result)
        __DoReload();
    }

    public void Execute_Reload(object sender)
    {
      __DoReload();
    }

    public void Execute_Cancel(object sender)
    {
      CurrentItem = m_OriginalCurrentItem;
    }

    private void __DoReload()
    {
      if (HasChanges)
        return;
      CurrentItems = DatenbankHelfer.ReadEntity<T>();
      if (CurrentItems != null && CurrentItems.Any())
        CurrentItem = CurrentItems.FirstOrDefault();
      OnPropertyChanged(nameof(CurrentItems));
    }

  }
}