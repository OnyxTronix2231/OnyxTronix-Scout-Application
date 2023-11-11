using System;
using Blazored.LocalStorage;

namespace OnyxScoutApplication.Client.Others.Managers;

public class AppManager
{
    private readonly ISyncLocalStorageService localStorageService;
    private bool isOnlineMode;

    public bool IsOnlineMode
    {
        get => isOnlineMode;
        set
        {
            localStorageService.SetItem("AppManager.IsOnlineMode", value);
            isOnlineMode = value;
        }
    }

    public AppManager(ISyncLocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
        if (!localStorageService.ContainKey("AppManager.IsOnlineMode"))
        {
            IsOnlineMode = true;
            return;
        }
        IsOnlineMode = localStorageService.GetItem<bool>("AppManager.IsOnlineMode");
    }
}