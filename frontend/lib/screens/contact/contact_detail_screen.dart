import 'package:flutter/material.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/services/contact_api_service.dart';

class ContactDetailScreen extends StatefulWidget {
  final int? contactId;

  const ContactDetailScreen({super.key,this.contactId});
  static const String id = "contact_detail_screen";
  @override
  _ContactDetailScreenState createState() => _ContactDetailScreenState();
}

class _ContactDetailScreenState extends State<ContactDetailScreen> {
  late Future<Contact> _contactFuture;

  @override
  void initState() {
    super.initState();
    _contactFuture = fetchContactDetail(widget.contactId!);
  }

  // Fetch a specific contact's details from the API
  Future<Contact> fetchContactDetail(int contactId) async {
    ContactApiService apiService = ContactApiService();
    return await apiService.getContactById(contactId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Contact Details'),
      ),
      body: FutureBuilder<Contact>(
        future: _contactFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData) {
            return Center(child: Text('No contact found.'));
          } else {
            Contact contact = snapshot.data!;
            return Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  CircleAvatar(
                    radius: 50,
                    backgroundImage: NetworkImage(contact.imageUrl ?? 'https://via.placeholder.com/150'),
                  ),
                  SizedBox(height: 16),
                  Text(
                    '${contact.firstName} ${contact.lastName}',
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text('Email: ${contact.email ?? 'N/A'}'),
                  Text('Phone: ${contact.phoneNumber ?? 'N/A'}'),
                  SizedBox(height: 16),
                  Text('Company: ${contact.companyName ?? 'N/A'}'),
                  Text('Job Title: ${contact.jobTitle ?? 'N/A'}'),
                  SizedBox(height: 16),
                  Text('Address:'),
                  Text(
                    '${contact.addressLine1 ?? 'N/A'}, ${contact.city ?? 'N/A'}, ${contact.state ?? 'N/A'}, ${contact.zipCode ?? 'N/A'}',
                  ),
                  Text('Country: ${contact.country ?? 'N/A'}'),
                  SizedBox(height: 16),
                  SizedBox(height: 16),
                  ElevatedButton(
                    onPressed: () {
                      // Here you can implement any update functionality if needed
                    },
                    child: Text('Update Contact'),
                  ),
                  ElevatedButton(
                    onPressed: () {
                      // Here you can implement any delete functionality if needed
                    },
                    child: Text('Delete Contact'),
                  ),
                ],
              ),
            );
          }
        },
      ),
    );
  }
}