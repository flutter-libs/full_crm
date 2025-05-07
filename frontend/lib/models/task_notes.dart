import 'package:frontend/models/tasks.dart';
import 'package:frontend/models/note.dart';

class TaskNotes {
  int? id;
  Tasks? task;
  Note? note;
  int? taskId;
  int? noteId;

  TaskNotes({
    this.id,
    this.task,
    this.note,
    this.taskId,
    this.noteId
  });

  factory TaskNotes.fromJson(Map<String, dynamic> json) {
    return TaskNotes(
      id: json['id'],
      taskId: json['taskId'],
      task: json['task'] != null ? Tasks.fromJson(json['task']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'task': task,
      'note': note,
      'noteId': noteId,
      'taskId': taskId
    };
  }
}