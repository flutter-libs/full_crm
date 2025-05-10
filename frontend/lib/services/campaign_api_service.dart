import 'package:dio/dio.dart';
import 'package:frontend/models/User.dart';
import 'package:frontend/models/campaign_notes.dart';
import 'package:frontend/models/campaign.dart';

class CampaignApiService {
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://192.168.1.6:8000/api/Main/Campaign',
    contentType: 'application/json',
  ));
  final Dio _userDio = Dio(BaseOptions(
    baseUrl: 'http://192.168.1.6:8000/api/Identity/User',
    contentType: 'application/json',
  ));

  Future<User> getCurrentUser() async {
    try {
      var user = await _userDio.get('/current-user');
      return User.fromJson(user.data);
    } catch (e) {
      throw Exception("Failed to fetch user: $e");
    }
  }
  // Get all campaigns
  Future<List<Campaign>> getAllCampaigns() async {
    try {
      final response = await _dio.get('/');
      List<dynamic> data = response.data;
      return data.map((json) => Campaign.fromJson(json)).toList();
    } catch (e) {
      throw Exception('Failed to fetch campaigns: $e');
    }
  }

  // Get a campaign by ID
  Future<Campaign> getCampaignById(int id) async {
    try {
      final response = await _dio.get('/$id');
      return Campaign.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch campaign: $e');
    }
  }

  // Create a new campaign
  Future<bool> createCampaign(Campaign campaign) async {
    try {
      final response = await _dio.post('/', data: campaign.toJson());
      if(response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        return false;
      }
    } catch (e) {
      throw Exception('Failed to create campaign: $e');
    }
  }

  // Update an existing campaign
  Future<Campaign> updateCampaign(int id, Campaign campaign) async {
    try {
      final response = await _dio.put('/$id', data: campaign.toJson());
      return Campaign.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update campaign: $e');
    }
  }

  // Delete a campaign
  Future<void> deleteCampaign(int id) async {
    try {
      await _dio.delete('/$id');
    } catch (e) {
      throw Exception('Failed to delete campaign: $e');
    }
  }

  Future<List<CampaignNotes>> getCampaignNotes() async {
    try {
      final response = await _dio.get('/notes');
      List<dynamic> data = response.data;
      return data.map((json) => CampaignNotes.fromJson(json)).toList();
    } catch (e) {
      throw Exception('Failed to fetch campaign notes: $e');
    }
  }

  Future<CampaignNotes> getCampaignNoteById(int id) async {
    try {
      final response = await _dio.get('/notes/$id');
      return CampaignNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to fetch campaign note: $e');
    }
  }

  Future<CampaignNotes> updateCampaignNote(int id, CampaignNotes note) async {
    try {
      final response = await _dio.put('/notes/$id', data: note.toJson());
      return CampaignNotes.fromJson(response.data);
    } catch (e) {
      throw Exception('Failed to update campaign note: $e');
    }
  }

  Future<void> deleteCampaignNote(int id) async {
    try {
      await _dio.delete('/notes/$id');
    } catch (e) {
      throw Exception('Failed to delete campaign note: $e');
    }
  }
}