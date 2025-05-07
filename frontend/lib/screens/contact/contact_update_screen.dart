import 'package:flutter/material.dart';


class ContactUpdateScreen extends StatefulWidget {
  final int? contactId;
  const ContactUpdateScreen({super.key, this.contactId});
  static const String id = "contact_update_screen";
  @override
  State<ContactUpdateScreen> createState() => _ContactUpdateScreenState();
}

class _ContactUpdateScreenState extends State<ContactUpdateScreen> {
  @override
  Widget build(BuildContext context) {
    return const Placeholder();
  }
}
