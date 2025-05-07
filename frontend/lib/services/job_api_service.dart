import 'package:frontend/models/contact_notes.dart';
import 'package:frontend/models/contact.dart';
import 'package:dio/dio.dart';
import 'package:frontend/models/job.dart';
import 'package:frontend/models/job_notes.dart';

class ContactApiService {
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://localhost:5244/api/Main/Job',
    contentType: 'application/json',
  ));

  Future<List<Job>> getAllContacts() async {
    try {
      final response = await _dio.get('/');
      return (response.data as List)
          .map((json) => Job.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch jobs: $e');
    }
  }

  Future<Job> getJobById(int id) async {
    try {
      final response = await _dio.get('/$id');
      return Job.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch job with ID $id: $e');
    }
  }

  Future<Job> createJob(Job job) async {
    try {
      final response = await _dio.post('/', data: job.toJson());
      return Job.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create contact: $e');
    }
  }

  Future<Job> updateJob(int id, Job job) async {
    try {
      final response = await _dio.put('/$id', data: job.toJson());
      return Job.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update job with ID $id: $e');
    }
  }

  Future<void> deleteJob(int id) async {
    try {
      await _dio.delete('/$id');
    } catch (e) {
      throw Exception('Failed to delete job with ID $id: $e');
    }
  }

  Future<int> countJobs() async {
    try {
      final response = await _dio.get('/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count jobs: $e');
    }
  }

  Future<List<JobNotes>> getAllJobNotes() async {
    try {
      final response = await _dio.get('/notes');
      return (response.data as List)
          .map((json) => JobNotes.fromJson(json))
          .toList();
    } catch (e) {
      throw Exception('Failed to fetch job notes: $e');
    }
  }

  Future<JobNotes> getJobNoteById(int id) async {
    try {
      final response = await _dio.get('/notes/$id');
      return JobNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch job note with ID $id: $e');
    }
  }

  Future<JobNotes> createJobNote(JobNotes note) async {
    try {
      final response = await _dio.post('/notes', data: note.toJson());
      return JobNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create job note: $e');
    }
  }

  Future<JobNotes> updateJobNote(int id, JobNotes note) async {
    try {
      final response = await _dio.put('/notes/$id', data: note.toJson());
      return JobNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update job note with ID $id: $e');
    }
  }

  Future<void> deleteJobNote(int id) async {
    try {
      await _dio.delete('/notes/$id');
    } catch (e) {
      throw Exception('Failed to delete job note with ID $id: $e');
    }
  }

  Future<int> countJobNotes() async {
    try {
      final response = await _dio.get('/notes/count');
      return response.data['count'] as int;
    } catch (e) {
      throw Exception('Failed to count contact notes: $e');
    }
  }
}