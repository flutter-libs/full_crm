import 'package:frontend/models/contact.dart';
import 'package:frontend/models/tasks.dart';

class Company {
  int? id;
  String? name;
  String? industry;
  String? website;
  String? address;
  String? city;
  String? state;
  String? country;
  String? zipCode;
  String? phoneNumber;
  String? fax;
  String? description;
  DateTime? dateCreated;
  List<Contact>? contacts;
  List<Tasks>? tasks;

  Company({
    this.id,
    this.name,
    this.industry,
    this.website,
    this.address,
    this.city,
    this.state,
    this.country,
    this.zipCode,
    this.phoneNumber,
    this.fax,
    this.description,
    this.dateCreated,
    this.contacts,
    this.tasks,
  });

  factory Company.fromJson(Map<String, dynamic> json) {
    return Company(
      id: json['id'],
      name: json['name'],
      industry: json['industry'],
      website: json['website'],
      address: json['address'],
      city: json['city'],
      state: json['state'],
      country: json['country'],
      zipCode: json['zipCode'],
      phoneNumber: json['phoneNumber'],
      fax: json['fax'],
      description: json['description'],
      dateCreated: DateTime.parse(json['dateCreated']),
      contacts: (json['contacts'] as List?)
          ?.map((item) => Contact.fromJson(item))
          .toList(),
      tasks: (json['tasks'] as List?)
          ?.map((item) => Tasks.fromJson(item))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'industry': industry,
      'website': website,
      'address': address,
      'city': city,
      'state': state,
      'country': country,
      'zipCode': zipCode,
      'phoneNumber': phoneNumber,
      'fax': fax,
      'description': description,
      'dateCreated': dateCreated?.toIso8601String(),
      'contacts': contacts?.map((e) => e.toJson()).toList(),
      'tasks': tasks?.map((e) => e.toJson()).toList(),
    };
  }
}