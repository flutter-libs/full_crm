import 'dart:convert';
import 'dart:io';
import 'package:frontend/models/user.dart';
import 'package:frontend/models/user_notes.dart';
import 'package:http/io_client.dart';


class UserAPIService {
  Future register(User user) async {
    String baseUrl = "http://192.168.1.248:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback = (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final url = Uri.parse('$baseUrl/register');
      final response = await http.post(
        url,
        headers: {'content-type': 'application/json'},
        body: jsonEncode({'userName': user.userName, 'email': user.email, 'name': user.name, 'address': user.address,
          'city': user.city, 'state': user.state, 'zipCode': user.zipCode, 'password': user.password,'dateOfBirth': user.dateOfBirth.toString()
        }),
      );
      if(response.statusCode == 200) {
        print(response.body);
      } else {
        print('A network error has occurred');
      }
    } catch (e) {
      throw Exception(e);
    }
  }

  Future login(String email, String password) async {
    String baseUrl = "http://192.168.1.248:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final url = Uri.parse('$baseUrl/login');
      final response = await http.post(
        url,
        headers: {'content-type': 'application/json'},
        body: jsonEncode({'email': email, 'password': password}),
      );

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        return data['token'];
      }
    } catch(e) {
      throw Exception(e);
    }
  }

  Future<bool> logout(String token) async {
    String baseUrl = "http://192.168.1.248:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final url = Uri.parse('$baseUrl/logout');
      final response = await http.post(
        url,
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $token',
        },
      );

      return response.statusCode == 200;
    } catch(e){
      throw Exception(e);
    }
  }

  Future<List<User>> getAllUsers() async {
    String baseUrl = "http://192.168.1.6:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final response = await http.get(Uri.parse(baseUrl));
      if (response.statusCode == 200) {
        List<dynamic> data = jsonDecode(response.body);
        return data.map((userJson) => User.fromJson(userJson)).toList();
      } else {
        throw Exception('Failed to load users');
      }
    } catch (e) {
      throw Exception(e);
    }
  }

  Future<User> getUserById(String id) async {
    String baseUrl = "http://192.168.1.6:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final response = await http.get(Uri.parse('$baseUrl/$id'));
      if (response.statusCode == 200) {
        return User.fromJson(jsonDecode(response.body));
      } else {
        throw Exception('User not found');
      }
    } catch (e) {
      throw Exception(e);
    }
  }

  Future<bool> updateUser(String id, User user) async {
    String baseUrl = "http://192.168.1.6:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final response = await http.put(
        Uri.parse('$baseUrl/$id'),
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode(user.toJson()),
      );
      return response.statusCode == 200;
    } catch (e) {
      throw Exception(e);
    }
  }

  Future<bool> deleteUser(String id) async {
    String baseUrl = "http://192.168.1.6:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final response = await http.delete(Uri.parse('$baseUrl/$id'));
      return response.statusCode == 200;
    } catch (e) {
      throw Exception(e);
    }
  }

  Future<int> countUsers() async {
    String baseUrl = "http://192.168.1.6:8000/api/Identity/User";
    try {
      final ioc = HttpClient();
      ioc.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      final http = IOClient(ioc);
      final response = await http.get(Uri.parse('$baseUrl/count'));
      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        return data['count'];
      } else {
        throw Exception('Failed to count users');
      }
    } catch (e) {
      throw Exception(e);
    }
  }
}


Future<List<UserNotes>> getAllUserNotes() async {
  String baseUrl = "http://192.168.1.6:8000/api/Main/Note/userNotes";
  try {
    final ioc = HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    final http = IOClient(ioc);
    final response = await http.get(Uri.parse(baseUrl));
    if (response.statusCode == 200) {
      List<dynamic> data = jsonDecode(response.body);
      return data.map((userJson) => UserNotes.fromJson(userJson)).toList();
    } else {
      throw Exception('Failed to load user notes');
    }
  } catch (e) {
    throw Exception(e);
  }
}

Future<UserNotes> getUserNoteById(String id) async {
  String baseUrl = "http://192.168.1.6:8000/api/Main/Note/userNotes";
  try {
    final ioc = HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    final http = IOClient(ioc);
    final response = await http.get(Uri.parse('$baseUrl/$id'));
    if (response.statusCode == 200) {
      return UserNotes.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('User note not found');
    }
  } catch (e) {
    throw Exception(e);
  }
}

Future<bool> updateUserNote(String id, UserNotes user) async {
  String baseUrl = "http://192.168.1.6:8000/api/Main/Note/userNotes";
  try {
    final ioc = HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    final http = IOClient(ioc);
    final response = await http.put(
      Uri.parse('$baseUrl/$id'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(user.toJson()),
    );
    return response.statusCode == 200;
  } catch (e) {
    throw Exception(e);
  }
}

Future<bool> deleteUserNote(String id) async {
  String baseUrl = "http://192.168.1.6:8000/api/Main/Note/userNotes";
  try {
    final ioc = HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    final http = IOClient(ioc);
    final response = await http.delete(Uri.parse('$baseUrl/$id'));
    return response.statusCode == 200;
  } catch (e) {
    throw Exception(e);
  }
}

Future<int> countUserNotes() async {
  String baseUrl = "http://192.168.1.6:8000/api/Main/Note/userNotes";
  try {
    final ioc = HttpClient();
    ioc.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
    final http = IOClient(ioc);
    final response = await http.get(Uri.parse('$baseUrl/count'));
    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return data['count'];
    } else {
      throw Exception('Failed to count users');
    }
  } catch (e) {
    throw Exception(e);
  }
}
