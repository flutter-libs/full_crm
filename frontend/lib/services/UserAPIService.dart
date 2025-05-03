import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:frontend/models/User.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;



class UserAPIService {
  final Dio _dio = Dio(BaseOptions(baseUrl: 'http://localhost:5244/api/Identity/User'));

  get context => BuildContext;

  Future<bool> register(User user) async {
    try {
      final response = await _dio.post('/register', data: user.toJson());
      return response.statusCode == 200 || response.statusCode == 201;
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Register Error');
      return false;
    }
  }

  Future<bool> login(String email, String password) async {
    try {
      final response = await _dio.post('/login', data: {
        'email': email,
        'password': password,
      });

      if (response.statusCode == 200) {
        final prefs = await SharedPreferences.getInstance();
        await prefs.setString('token', response.data['token']);
        return true;
      }

      return false;
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Login Error');
      return false;
    }
  }

  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('token');
  }

  Future<List<User>> getAllUsers() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List).map((json) => User.fromJson(json)).toList();
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Could not retrieve all users.');
      return [];
    }
  }

  Future<User?> getUserById(String id) async {
    try {
      final response = await _dio.get('/$id');
      return User.fromJson(response.data);
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Could not get the user by id: $id');
      return null;
    }
  }

  Future<bool> updateUser(String id, User user) async {
    try {
      final response = await _dio.put('/$id', data: user.toJson());
      return response.statusCode == 200;
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Could not update the user.');
      return false;
    }
  }

  Future<bool> deleteUser(String id) async {
    try {
      final response = await _dio.delete('/$id');
      return response.statusCode == 200;
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Could not delete the user.');
      return false;
    }
  }

  Future<int> countAllUsers() async {
    try {
      final response = await _dio.get('/count');
      return response.data['count'] ?? 0;
    } catch (e) {
      alert.showErrorToast(context, '$e', 'Could not retrieve the count of users.');
      return 0;
    }
  }
}