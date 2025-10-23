using Business.Services;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using NguyenNguyenNguWPF.Views;

public class RoomsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    private readonly RoomService _service = new();

    public ObservableCollection<RoomInformation> Rooms { get; } = new();

    private string _searchKeyword = string.Empty;
    public string SearchKeyword
    {
        get => _searchKeyword;
        set
        {
            if (SetProperty(ref _searchKeyword, value))
                (SearchCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    private RoomInformation? _selectedRoom;
    public RoomInformation? SelectedRoom
    {
        get => _selectedRoom;
        set
        {
            if (SetProperty(ref _selectedRoom, value))
                CommandManager.InvalidateRequerySuggested();
        }
    }

    public ICommand SearchCommand { get; }
    public RelayCommand AddNewRoomCommand { get; }
    public RelayCommand EditRoomCommand { get; }
    public RelayCommand DeleteRoomCommand { get; }

    public RoomsViewModel()
    {
        foreach (var r in _service.GetAll()) Rooms.Add(r);

        SearchCommand = new RelayCommand(Search, _ => true);
        AddNewRoomCommand = new RelayCommand(ExecuteAddNewRoom);
        EditRoomCommand = new RelayCommand(ExecuteEditRoom, CanEditOrDeleteRoom);
        DeleteRoomCommand = new RelayCommand(ExecuteDeleteRoom, CanEditOrDeleteRoom);
    }

    private bool CanEditOrDeleteRoom(object? _) => SelectedRoom != null;

    private void Search(object? _)
    {
        var results = _service.Search(SearchKeyword?.Trim() ?? string.Empty);
        Rooms.Clear();
        foreach (var r in results) Rooms.Add(r);
    }

    private void ExecuteAddNewRoom(object? _)
    {
        var editor = AddEditRoomViewModel.CreateForAdd();
        OpenAddEditDialog(editor, onSaved: (saved) =>
        {
            _service.Add(saved);
            RefreshListAndReselect(saved.RoomID);
        });
    }

    private void ExecuteEditRoom(object? _)
    {
        if (SelectedRoom == null) return;

        var editor = AddEditRoomViewModel.CreateForEdit(SelectedRoom);
        OpenAddEditDialog(editor, onSaved: (saved) =>
        {
            _service.Update(saved);
            RefreshListAndReselect(saved.RoomID);
        });
    }

    private void ExecuteDeleteRoom(object? _)
    {
        if (SelectedRoom == null) return;

        var answer = MessageBox.Show(
            $"Delete room \"{SelectedRoom.RoomNumber}\"?",
            "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (answer != MessageBoxResult.Yes) return;

        _service.Delete(SelectedRoom.RoomID);
        RefreshListAndReselect(null);
    }

    private void OpenAddEditDialog(AddEditRoomViewModel vm, System.Action<RoomInformation> onSaved)
    {
        var dlg = new AddEditRoomWindow
        {
            DataContext = vm,
            Owner = Application.Current?.MainWindow
        };

        var result = dlg.ShowDialog();
        if (result == true)
        {
            var entity = vm.ToEntity();
            onSaved(entity);
        }
    }

    private void RefreshListAndReselect(int? roomId)
    {
        Rooms.Clear();
        foreach (var r in _service.GetAll()) Rooms.Add(r);

        SelectedRoom = roomId.HasValue ? Rooms.FirstOrDefault(x => x.RoomID == roomId.Value) : null;
    }
}