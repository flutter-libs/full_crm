import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:frontend/models/user_meeting.dart';


class MeetingAPIService {
  final Dio _dio = Dio();
  final String baseUrl = "http://192.168.1.6:8000/api/Communication/Meeting";
  // Get all meetings
  Future<List<UserMeeting>> getMeetings() async {
    try {
      final response = await _dio.get(baseUrl);
      final List<dynamic> data = response.data;
      return data.map((meetingJson) => UserMeeting.fromJson(meetingJson)).toList();
    } catch (e) {
      throw Exception('Failed to load meeting');
    }
  }

  // Get meeting by id
  Future<UserMeeting> getMeetingById(int id) async {
    try {
      final response = await _dio.get('$baseUrl/$id');
      return UserMeeting.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to load meeting');
    }
  }

  // Create a new meeting
  Future<UserMeeting> createMeeting(Map<String, dynamic> meetingData) async {
    try {
      final response = await _dio.post(baseUrl, data: jsonEncode(meetingData));
      return UserMeeting.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to create meeting');
    }
  }

  // Add participants to a meeting
  Future<bool> addParticipants(int meetingId, List<String> userIds) async {
    try {
      final response = await _dio.post(
        '$baseUrl/$meetingId/participants',
        data: jsonEncode(userIds),
      );
      return response.statusCode == 200;
    } catch (e) {
      throw Exception('Failed to create meeting');
    }
  }

  // Update meeting details
  Future<UserMeeting> updateMeeting(int id, Map<String, dynamic> updatedData) async {
    try {
      final response = await _dio.put('$baseUrl/$id', data: jsonEncode(updatedData));
      return UserMeeting.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update meeting');
    }
  }

  // Remove participant from a meeting
  Future<bool> removeParticipant(int meetingId, String userId) async {
    try {
      final response = await _dio.delete('$baseUrl/$meetingId/participants/$userId');
      return response.statusCode == 204;
    } catch (e) {
      throw Exception('Failed to remove user from meeting');
    }
  }

  // Delete a meeting
  Future<bool> deleteMeeting(int meetingId) async {
    try {
      final response = await _dio.delete('$baseUrl/$meetingId');
      return response.statusCode == 200;
    } catch (e) {
      throw Exception('Failed to remove meeting');
    }
  }

  // Get meeting count
  Future<int> getMeetingCount() async {
    try {
      final response = await _dio.get('$baseUrl/count');
      return response.data as int;
    } catch (e) {
      throw Exception('Failed to fetch meeting count');
    }
  }
}