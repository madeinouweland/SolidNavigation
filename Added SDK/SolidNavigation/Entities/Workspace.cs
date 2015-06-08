using System.Collections.Generic;

namespace SolidNavigation.Entities {
    public class Workspace {
        private static Workspace _current;
        public static Workspace Current { get { return _current ?? (_current = new Workspace()); } }

        public List<ListModel> Lists { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<CommentModel> Comments { get; set; }

        public Workspace() {
            Lists = new List<ListModel>
            {
                new ListModel {Id = 1, Title = "Music"},
                new ListModel {Id = 2, Title = "Restaurants"},
                new ListModel {Id = 3, Title = "Movies"}
            };

            Tasks = new List<TaskModel>
            {
                new TaskModel {Id = 1, ListId = 1, Title = "The Beatles"},
                new TaskModel {Id = 2, ListId = 1, Title = "Steely Dan"},
                new TaskModel {Id = 3, ListId = 1, Title = "R.E.M."},
                new TaskModel {Id = 4, ListId = 1, Title = "Fleetwood Mac"},
                new TaskModel {Id = 5, ListId = 1, Title = "Dota und die Stadtpiraten"},

                new TaskModel {Id = 6, ListId = 2, Title = "Chai Village"},
                new TaskModel {Id = 7, ListId = 2, Title = "The Bird"},
                new TaskModel {Id = 8, ListId = 2, Title = "Lemongrass"},

                new TaskModel {Id = 9, ListId = 3, Title = "The godfather"},
                new TaskModel {Id = 10, ListId = 3, Title = "Back to the future"},
                new TaskModel {Id = 11, ListId = 3, Title = "Casino"}
            };

            Comments = new List<CommentModel>();
            foreach (var task in Tasks) {
                for (int i = 0; i < 10; i++) {
                    Comments.Add(new CommentModel { Id = i * task.Id, TaskId = task.Id, Text = "Comment " + i * task.Id });
                }
            }
        }
    }
}
