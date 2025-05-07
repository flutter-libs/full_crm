import 'package:frontend/models/campaign.dart';
import 'package:frontend/models/note.dart';

class CampaignNotes {
  int? id;
  Campaign? campaign;
  Note? note;
  int? campaignId;
  int? noteId;

  CampaignNotes({
    this.id,
    this.campaign,
    this.note,
    this.campaignId,
    this.noteId
  });

  factory CampaignNotes.fromJson(Map<String, dynamic> json) {
    return CampaignNotes(
      id: json['id'],
      campaignId: json['campaignId'],
      campaign: json['campaign'] != null ? Campaign.fromJson(json['campaign']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'campaign': campaign,
      'note': note,
      'noteId': noteId,
      'campaignId': campaignId
    };
  }
}