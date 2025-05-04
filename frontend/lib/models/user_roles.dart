import 'package:frontend/models/User.dart';
import 'package:frontend/models/role.dart';

class UserRoles {
  int? id;
  String? userId;
  String? roleId;
  User? user;
  Role? role;

  UserRoles({
    this.id,
    this.userId,
    this.roleId,
    this.user,
    this.role,
  });

  factory UserRoles.fromJson(Map<String, dynamic> json) {
    return UserRoles(
      id: json['id'],
      userId: json['userId'],
      roleId: json['roleId'],
      user: json['user'] != null ? User.fromJson(json['user']) : null,
      role: json['role'] != null ? Role.fromJson(json['role']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userId': userId,
      'roleId': roleId,
      'user': user?.toJson(),
      'role': role?.toJson(),
    };
  }
}