using System.Collections.Generic;
using System.Linq;
using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using SolidNavigation.Tasks;

namespace SolidNavigation.Details {
    public class TaskDetailsPageViewModel : ObservableObject {
        private TaskViewModel _task;
        public TaskViewModel Task { get { return _task; } }
        public List<CommentViewModel> Comments { get; set; }
        private CommentViewModel _selectedComment;

        public TaskDetailsPageViewModel(long taskId) {
            LoadData(taskId);
        }

        public TaskDetailsPageViewModel(long taskId, long commentId) {
            LoadData(taskId);
            _selectedComment = Comments.FirstOrDefault(x => x.Id == commentId);
        }

        private void LoadData(long taskId) {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Id = taskId, Title = task.Title };

            Comments = (from comment in Workspace.Current.Comments.Where(x => x.TaskId == _task.Id)
                        select new CommentViewModel { Text = comment.Text, Id = comment.Id, }).ToList();
        }

        public object SelectedComment {
            get { return _selectedComment; }
            set {
                var newValue = (CommentViewModel)value;
                if (newValue != null && newValue != _selectedComment) {
                    _selectedComment = newValue;
                    NavigateService.Current.Navigate(new CommentTarget(_task.Id, _selectedComment.Id));
                }
            }
        }
    }
}
