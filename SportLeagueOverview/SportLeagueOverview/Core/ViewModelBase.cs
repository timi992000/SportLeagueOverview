using SportLeagueOverview.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SportLeagueOverview.Core
{
  public class ViewModelBase<T> : INotifyPropertyChanged
  {
    private T m_CurrentItem;
    private object m_ViewModel;
    public ViewModelBase(object ViewModel = null)
    {
      CurrentItems = DatenbankHelfer.ReadEntity<T>();
      if (CurrentItems != null && CurrentItems.Any())
        CurrentItem = CurrentItems.FirstOrDefault();
      m_ViewModel = ViewModel;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        //if (!PropertyName.Equals(nameof(CurrentItems)))
        //{
        //  OnPropertyChanged(nameof(CurrentItems));
        //}
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
        OnPropertyChanged();
        OnCurrentItemChanged();
      }
    }

  }
}