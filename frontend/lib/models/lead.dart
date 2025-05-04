import 'package:frontend/models/User.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/models/meeting.dart';

class Lead {
  int? id;
  String? leadName;
  String? leadAddress;
  String? leadCity;
  String? leadState;
  String? leadZip;
  String? leadCountry;
  String? leadPhone;
  String? leadEmail;
  String? leadFax;
  String? leadWebsite;
  String? leadNotes;
  DateTime? created;
  DateTime? updated;
  String? createdBy;
  User? createdByUser;
  List<Campaign>? campaigns;
  List<Meeting>? meetings;

  Lead({
    this.id,
    this.leadName,
    this.leadAddress,
    this.leadCity,
    this.leadState,
    this.leadZip,
    this.leadCountry,
    this.leadPhone,
    this.leadEmail,
    this.leadFax,
    this.leadWebsite,
    this.leadNotes,
    this.created,
    this.updated,
    this.createdBy,
    this.createdByUser,
    this.campaigns,
    this.meetings,
  });

  factory Lead.fromJson(Map<String, dynamic> json) {
    return Lead(
      id: json['id'],
      leadName: json['leadName'],
      leadAddress: json['leadAddress'],
      leadCity: json['leadCity'],
      leadState: json['leadState'],
      leadZip: json['leadZip'],
      leadCountry: json['leadCountry'],
      leadPhone: json['leadPhone'],
      leadEmail: json['leadEmail'],
      leadFax: json['leadFax'],
      leadWebsite: json['leadWebsite'],
      leadNotes: json['leadNotes'],
      created: DateTime.parse(json['created']),
      updated: DateTime.parse(json['updated']),
      createdBy: json['createdBy'],
      createdByUser: json['createdByUser'] != null
          ? User.fromJson(json['createdByUser'])
          : null,
      campaigns: (json['campaigns'] as List?)
          ?.map((item) => Campaign.fromJson(item))
          .toList(),
      meetings: (json['meetings'] as List?)
          ?.map((item) => Meeting.fromJson(item))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'leadName': leadName,
      'leadAddress': leadAddress,
      'leadCity': leadCity,
      'leadState': leadState,
      'leadZip': leadZip,
      'leadCountry': leadCountry,
      'leadPhone': leadPhone,
      'leadEmail': leadEmail,
      'leadFax': leadFax,
      'leadWebsite': leadWebsite,
      'leadNotes': leadNotes,
      'created': created?.toIso8601String(),
      'updated': updated?.toIso8601String(),
      'createdBy': createdBy,
      'createdByUser': createdByUser?.toJson(),
      'campaigns': campaigns?.map((e) => e.toJson()).toList(),
      'meetings': meetings?.map((e) => e.toJson()).toList(),
    };
  }
}