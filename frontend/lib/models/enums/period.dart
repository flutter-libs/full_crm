enum Period {
  Daily,
  Weekly,
  Monthly,
  Yearly,
}

extension PeriodExtension on Period {
  // Convert a string or integer value to the Period enum
  static Period fromJson(dynamic json) {
    switch (json) {
      case 1:
        return Period.Daily;
      case 2:
        return Period.Weekly;
      case 3:
        return Period.Monthly;
      case 4:
        return Period.Yearly;
      default:
        throw ArgumentError('Invalid period value: $json');
    }
  }

  // Convert the Period enum to a numeric value for JSON
  int toJson() {
    switch (this) {
      case Period.Daily:
        return 1;
      case Period.Weekly:
        return 2;
      case Period.Monthly:
        return 3;
      case Period.Yearly:
        return 4;
    }
  }
}