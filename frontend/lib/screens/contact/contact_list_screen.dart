import 'package:flutter/material.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/screens/contact/contact_detail_screen.dart';
import 'package:frontend/services/contact_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;


class ContactListScreen extends StatefulWidget {
  const ContactListScreen({super.key});
  static const String id = "contact_list_screen";
  @override
  State<ContactListScreen> createState() => _ContactListScreenState();
}

class _ContactListScreenState extends State<ContactListScreen> {
  late Future<List<Contact>> _contactsFuture;

  @override
  void initState() {
    super.initState();
    _contactsFuture = fetchContacts();
  }
  Future<List<Contact>> fetchContacts() async {
    ContactApiService _apiService = ContactApiService();
    return await _apiService.getAllContacts();
  }
    @override
    Widget build(BuildContext context) {
      return Scaffold(
        appBar: CustomAppBar(
          title: 'Contacts',
        ),
        drawer: SideNavDrawer(),
        body: FutureBuilder<List<Contact>>(
          future: _contactsFuture,
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return Center(child: CircularProgressIndicator());
            } else if (snapshot.hasError) {
              return Center(child: Text('Error: ${snapshot.error}'));
            } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
              return Center(child: Text('No contacts found.'));
            } else {
              List<Contact> contacts = snapshot.data!;
              return ListView.builder(
                itemCount: contacts.length,
                itemBuilder: (context, index) {
                  final contact = contacts[index];
                  return Card(
                    margin: EdgeInsets.symmetric(vertical: 8, horizontal: 16),
                    child: ListTile(
                      leading: CircleAvatar(
                        backgroundImage: NetworkImage(contact.imageUrl ?? 'https://via.placeholder.com/150'),
                      ),
                      title: Text('${contact.firstName} ${contact.lastName}'),
                      subtitle: Text(contact.email ?? 'No email'),
                      trailing: IconButton(
                        icon: Icon(Icons.arrow_forward),
                        onPressed: () {
                          // Navigate to the contact detail screen
                          // Pass the contact id or contact object to the next screen
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => ContactDetailScreen(contactId: contact.id),
                            ),
                          );
                        },
                      ),
                    ),
                  );
                },
              );
            }
          },
        ),
      );
  }
}
