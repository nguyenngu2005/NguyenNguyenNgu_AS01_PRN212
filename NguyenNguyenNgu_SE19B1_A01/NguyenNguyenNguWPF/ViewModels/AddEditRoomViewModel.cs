using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using System.Collections.Generic; 

public class AddEditRoomViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        SaveCommand?.RaiseCanExecuteChanged();
        return true;
    }

    public bool IsEdit { get; private set; }
    public int RoomID { get; private set; }

    private string _roomNumber = "";
    public string RoomNumber { get => _roomNumber; set => SetProperty(ref _roomNumber, value); }

    private string _roomDescription = "";
    public string RoomDescription { get => _roomDescription; set => SetProperty(ref _roomDescription, value); }

    private int _roomTypeID;
    public int RoomTypeID { get => _roomTypeID; set => SetProperty(ref _roomTypeID, value); }

    private int _roomMaxCapacity = 1;
    public int RoomMaxCapacity { get => _roomMaxCapacity; set => SetProperty(ref _roomMaxCapacity, value); }

    private decimal _roomPricePerDay;
    public decimal RoomPricePerDay { get => _roomPricePerDay; set => SetProperty(ref _roomPricePerDay, value); }

    private int _roomStatus;
    public int RoomStatus { get => _roomStatus; set => SetProperty(ref _roomStatus, value); }
    public RelayCommand? SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public Action<bool>? CloseAction { get; set; }

    private AddEditRoomViewModel()
    {
        SaveCommand = new RelayCommand(_ => CloseAction?.Invoke(true), _ => CanSave());
        CancelCommand = new RelayCommand(_ => CloseAction?.Invoke(false));
    }

    public static AddEditRoomViewModel CreateForAdd()
    {
        return new AddEditRoomViewModel
        {
            IsEdit = false,
            RoomStatus = 1,
            RoomMaxCapacity = 1
        };
    }

    public static AddEditRoomViewModel CreateForEdit(RoomInformation src)
    {
        return new AddEditRoomViewModel
        {
            IsEdit = true,
            RoomID = src.RoomID,
            RoomNumber = src.RoomNumber,
            RoomDescription = src.RoomDetailDescription ?? string.Empty,
            RoomTypeID = src.RoomTypeID,
            RoomMaxCapacity = src.RoomMaxCapacity,
            RoomPricePerDay = src.RoomPricePerDay,
            RoomStatus = src.RoomStatus
        };
    }

    public bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(RoomNumber)
            && RoomTypeID > 0
            && RoomMaxCapacity > 0
            && RoomPricePerDay >= 0;
    }

    public RoomInformation ToEntity()
    {
        return new RoomInformation
        {
            RoomID = RoomID,
            RoomNumber = RoomNumber.Trim(),
            RoomDetailDescription = RoomDescription.Trim(),
            RoomTypeID = RoomTypeID,
            RoomMaxCapacity = RoomMaxCapacity,
            RoomPricePerDay = RoomPricePerDay,
            RoomStatus = (byte)RoomStatus 
        };
    }
}