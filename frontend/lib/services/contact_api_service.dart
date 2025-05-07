import 'package:frontend/models/contact_notes.dart';
import 'package:frontend/models/contact.dart';
import 'package:dio/dio.dart';

class ContactApiService {
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://localhost:5244/api/Main/Contact',
    contentType: 'application/json',
  ));

  Future<List<Contact>> getAllContacts() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List)
          .map((json) => Contact.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch contacts: $e');
    }
  }

  Future<Contact> getContactById(int id) async {
    try {
      final response = await _dio.get('/$id');
      return Contact.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch contact with ID $id: $e');
    }
  }

  Future<bool> createContact(Contact contact) async {
    try {
      final response = await _dio.post('/', data: contact.toJson());
      if(response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        return false;
      }
    } catch (e) {
      throw Exception('Failed to create contact: $e');
    }
  }

  Future<Contact> updateContact(int id, Contact contact) async {
    try {
      final response = await _dio.put('/$id', data: contact.toJson());
      return Contact.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update contact with ID $id: $e');
    }
  }

  Future<void> deleteContact(int id) async {
    try {
      await _dio.delete('/$id');
    } catch (e) {
      throw Exception('Failed to delete contact with ID $id: $e');
    }
  }

  Future<int> countContacts() async {
    try {
      final response = await _dio.get('/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count leads: $e');
    }
  }

  Future<List<ContactNotes>> getAllContactNotes() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List)
          .map((json) => ContactNotes.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch contact notes: $e');
    }
  }

  Future<ContactNotes> getContactNoteById(int id) async {
    try {
      final response = await _dio.get('/notes/$id');
      return ContactNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch contact note with ID $id: $e');
    }
  }

  Future<ContactNotes> createContactNote(ContactNotes note) async {
    try {
      final response = await _dio.post('/notes', data: note.toJson());
      return ContactNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create contact note: $e');
    }
  }

  Future<ContactNotes> updateContactNote(int id, ContactNotes note) async {
    try {
      final response = await _dio.put('/notes/$id', data: note.toJson());
      return ContactNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update contact note with ID $id: $e');
    }
  }

  Future<void> deleteContactNote(int id) async {
    try {
      await _dio.delete('/notes/$id');
    } catch (e) {
      throw Exception('Failed to delete contact note with ID $id: $e');
    }
  }

  Future<int> countContactNotes() async {
    try {
      final response = await _dio.get('/notes/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count contact notes: $e');
    }
  }
}