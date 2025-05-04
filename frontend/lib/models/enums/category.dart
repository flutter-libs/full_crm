enum Category {
  System,
  User,
  Role,
  Lead,
  Campaign,
  Contact,
  Job,
  Revenue,
  Maintenance,
}

extension CategoryExtension on Category {
  // Convert a string or integer value to the Category enum
  static Category fromJson(dynamic json) {
    switch (json) {
      case 1:
        return Category.System;
      case 2:
        return Category.User;
      case 3:
        return Category.Role;
      case 4:
        return Category.Lead;
      case 5:
        return Category.Campaign;
      case 6:
        return Category.Contact;
      case 7:
        return Category.Job;
      case 8:
        return Category.Revenue;
      case 9:
        return Category.Maintenance;
      default:
        throw ArgumentError('Invalid category value: $json');
    }
  }

  // Convert the Category enum to a numeric value for JSON
  int toJson() {
    switch (this) {
      case Category.System:
        return 1;
      case Category.User:
        return 2;
      case Category.Role:
        return 3;
      case Category.Lead:
        return 4;
      case Category.Campaign:
        return 5;
      case Category.Contact:
        return 6;
      case Category.Job:
        return 7;
      case Category.Revenue:
        return 8;
      case Category.Maintenance:
        return 9;
    }
  }
}