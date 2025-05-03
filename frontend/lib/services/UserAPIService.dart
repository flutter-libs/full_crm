import 'package:dio/dio.dart';
import 'package:frontend/models/User.dart';
import 'package:shared_preferences/shared_preferences.dart';

class UserAPIService {
  final Dio _dio = Dio(BaseOptions(baseUrl: 'http://your-api-url.com/api/users'));

  Future<bool> register(User user) async {
    try {
      final response = await _dio.post('/register', data: user.toJson());
      return response.statusCode == 200 || response.statusCode == 201;
    } catch (e) {
      print('Register error: $e');
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
      print('Login error: $e');
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
      print('Get all users error: $e');
      return [];
    }
  }

  Future<User?> getUserById(String id) async {
    try {
      final response = await _dio.get('/$id');
      return User.fromJson(response.data);
    } catch (e) {
      print('Get user by ID error: $e');
      return null;
    }
  }

  Future<bool> updateUser(String id, User user) async {
    try {
      final response = await _dio.put('/$id', data: user.toJson());
      return response.statusCode == 200;
    } catch (e) {
      print('Update user error: $e');
      return false;
    }
  }

  Future<bool> deleteUser(String id) async {
    try {
      final response = await _dio.delete('/$id');
      return response.statusCode == 200;
    } catch (e) {
      print('Delete user error: $e');
      return false;
    }
  }

  Future<int> countAllUsers() async {
    try {
      final response = await _dio.get('/count');
      return response.data['count'] ?? 0;
    } catch (e) {
      print('Count users error: $e');
      return 0;
    }
  }
}