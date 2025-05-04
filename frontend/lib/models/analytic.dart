import 'package:frontend/models/User.dart';
import 'package:frontend/models/enums/category.dart';
import 'package:frontend/models/enums/period.dart';

class Analytic {
  int? id;
  Category? category;
  String? metricName;
  String? description;
  double? value;
  Period? period;
  DateTime? recordedDate;
  String? createdByUserId;
  User? createdByUser;
  DateTime? created;
  DateTime? updated;

  Analytic({
    this.id,
    this.category,
    this.metricName,
    this.description,
    this.value,
    this.period,
    this.recordedDate,
    this.createdByUserId,
    this.createdByUser,
    this.created,
    this.updated,
  });

  factory Analytic.fromJson(Map<String, dynamic> json) {
    return Analytic(
      id: json['id'],
      category: json['category'] != null ? CategoryExtension.fromJson(json['category']) : null,
      metricName: json['metricName'],
      description: json['description'],
      value: json['value']?.toDouble(),
      period: json['period'] != null ? PeriodExtension.fromJson(json['period']) : null,
      recordedDate: json['recordedDate'] != null ? DateTime.parse(json['recordedDate']) : null,
      createdByUserId: json['createdByUserId'],
      createdByUser: json['createdByUser'] != null ? User.fromJson(json['createdByUser']) : null,
      created: json['created'] != null ? DateTime.parse(json['created']) : null,
      updated: json['updated'] != null ? DateTime.parse(json['updated']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'category': category?.toJson(),
      'metricName': metricName,
      'description': description,
      'value': value,
      'period': period?.toJson(),
      'recordedDate': recordedDate?.toIso8601String(),
      'createdByUserId': createdByUserId,
      'createdByUser': createdByUser?.toJson(),
      'created': created?.toIso8601String(),
      'updated': updated?.toIso8601String(),
    };
  }
}