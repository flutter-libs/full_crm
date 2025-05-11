import 'dart:convert';
import 'dart:io';
import 'package:dio/dio.dart';
import 'package:frontend/models/user.dart';
import 'package:frontend/models/user_notes.dart';
import 'package:http/io_client.dart';


class UserApiService {
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://192.168.1.248/api/Identity/User',
    connectTimeout: const Duration(seconds: 10),
    receiveTimeout: const Duration(seconds: 10),
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    },
  ));

  String? _authToken;

  void setAuthToken(String token) {
    _authToken = token;
    _dio.options.headers['Authorization'] = 'Bearer $token';
  }

  void clearAuthToken() {
    _authToken = null;
    _dio.options.headers.remove('Authorization');
  }

  // Register user
  Future<Response> register(Map<String, dynamic> userData) async {
    return await _dio.post('/register', data: userData);
  }

  // Login
  Future<Response> login(String email, String password) async {
    final response = await _dio.post('/login', data: {
      'email': email,
      'password': password,
    });

    if (response.statusCode == 200 && response.data['token'] != null) {
      setAuthToken(response.data['token']);
    }

    return response;
  }

  // Logout (optional API support)
  Future<Response> logout() async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.post('/logout');
  }

  // Get all users
  Future<Response> getAllUsers() async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.get('/');
  }

  // Get user by ID
  Future<Response> getUserById(String id) async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.get('/$id');
  }

  // Update user
  Future<Response> updateUser(String id, Map<String, dynamic> updatedData) async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.put('/$id', data: updatedData);
  }

  // Delete user
  Future<Response> deleteUser(String id) async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.delete('/$id');
  }

  // Count all users
  Future<Response> countAllUsers() async {
    if (_authToken == null) throw Exception("User not logged in.");
    return await _dio.get('/count');
  }
}

