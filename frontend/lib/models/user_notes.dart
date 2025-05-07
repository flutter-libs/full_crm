import 'package:frontend/models/User.dart';
import 'package:frontend/models/note.dart';

class UserNotes {
  int? id;
  User? user;
  Note? note;
  String? userId;
  int? noteId;

  UserNotes({
    this.id,
    this.user,
    this.note,
    this.userId,
    this.noteId
  });

  factory UserNotes.fromJson(Map<String, dynamic> json) {
    return UserNotes(
      id: json['id'],
      userId: json['userId'],
      user: json['user'] != null ? User.fromJson(json['user']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'user': user,
      'note': note,
      'noteId': noteId,
      'userId': userId
    };
  }
}