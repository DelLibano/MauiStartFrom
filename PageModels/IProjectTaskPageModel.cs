using CommunityToolkit.Mvvm.Input;

namespace MauiStartFrom.PageModels;

public interface IProjectTaskPageModel
{
    IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
    bool IsBusy { get; }
}