import 'package:frontend/models/job.dart';
import 'package:frontend/models/note.dart';

class JobNotes {
  int? id;
  Job? job;
  Note? note;
  int? jobId;
  int? noteId;

  JobNotes({
    this.id,
    this.job,
    this.note,
    this.jobId,
    this.noteId
  });

  factory JobNotes.fromJson(Map<String, dynamic> json) {
    return JobNotes(
      id: json['id'],
      jobId: json['jobId'],
      job: json['job'] != null ? Job.fromJson(json['job']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'job': job,
      'note': note,
      'noteId': noteId,
      'jobId': jobId
    };
  }
}