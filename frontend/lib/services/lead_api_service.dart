import 'package:dio/dio.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/models/lead_notes.dart';

class LeadApiService {
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://localhost:5244/api/Main/Lead',
    contentType: 'application/json',
  ));

  // Get all leads
  Future<List<Lead>> getAllLeads() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List)
          .map((json) => Lead.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch leads: $e');
    }
  }

  // Get lead by ID
  Future<Lead> getLeadById(int id) async {
    try {
      final response = await _dio.get('/$id');
      return Lead.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch lead with ID $id: $e');
    }
  }

  // Create a new lead
  Future<Lead> createLead(Lead lead) async {
    try {
      final response = await _dio.post('/', data: lead.toJson());
      return Lead.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create lead: $e');
    }
  }

  // Update a lead
  Future<Lead> updateLead(int id, Lead lead) async {
    try {
      final response = await _dio.put('/$id', data: lead.toJson());
      return Lead.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update lead with ID $id: $e');
    }
  }

  // Delete a lead
  Future<void> deleteLead(int id) async {
    try {
      await _dio.delete('/$id');
    } catch (e) {
      throw Exception('Failed to delete lead with ID $id: $e');
    }
  }

  // Optional: Count all leads
  Future<int> countLeads() async {
    try {
      final response = await _dio.get('/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count leads: $e');
    }
  }



  // Get all lead notes
  Future<List<LeadNotes>> getAllLeadNotes() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List)
          .map((json) => LeadNotes.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch lead notes: $e');
    }
  }

  // Get lead note by ID
  Future<LeadNotes> getLeadNoteById(int id) async {
    try {
      final response = await _dio.get('/notes/$id');
      return LeadNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch lead note with ID $id: $e');
    }
  }

  // Create a new lead note
  Future<LeadNotes> createLeadNote(LeadNotes lead) async {
    try {
      final response = await _dio.post('/notes', data: lead.toJson());
      return LeadNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create lead note: $e');
    }
  }

  // Update a lead note
  Future<LeadNotes> updateLeadNote(int id, LeadNotes lead) async {
    try {
      final response = await _dio.put('/notes/$id', data: lead.toJson());
      return LeadNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update lead with ID $id: $e');
    }
  }

  // Delete a lead note
  Future<void> deleteLeadNote(int id) async {
    try {
      await _dio.delete('/notes/$id');
    } catch (e) {
      throw Exception('Failed to delete lead note with ID $id: $e');
    }
  }

  // Optional: Count all leads notes
  Future<int> countLeadNotes() async {
    try {
      final response = await _dio.get('/notes/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count lead notes: $e');
    }
  }
}