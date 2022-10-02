#include "Connection.h"
#include "Logger.h"
#include <iostream>
#include <fstream>
#include <string>

Connection::Connection(char* hostname, unsigned short port, ConnectionParameters* parameters) {
    this->hostname = hostname;
    this->port = port;

    for (unsigned int i = 0; i < parameters->ClientComponentVtableCount; ++i) {
        this->vtable[parameters->ClientComponentVtable[i].ComponentId] = parameters->ClientComponentVtable[i];
        Logger::Debug("Component registered: " + std::to_string(parameters->ClientComponentVtable[i].ComponentId));
    }
}

bool Connection::IsConnected()
{
    return false;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    if (!this->didLoadPlayer) {
        op_list->assetLoadRequestOp = new AssetLoadRequestOp();

        // prefab name gets assembled by game to "Traveller@Player_unityclient"
        op_list->assetLoadRequestOp->AssetType = (char*)"OnlyForResponse?";
        op_list->assetLoadRequestOp->Name = (char*)"Traveller";
        op_list->assetLoadRequestOp->Context = (char*)"Player";

        this->didLoadPlayer = true;
    }
    else if (!this->didCreatePlayer && this->gameLoadedPlayer) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 1;
        op_list->addEntityOp->PrefabName = (char*)"Traveller";
        op_list->addEntityOp->PrefabContext = (char*)"Player";

        this->didCreatePlayer = true;
    }
    else if (!this->didSendAddEntityRequest) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 2;
        op_list->addEntityOp->PrefabName = (char*)"949069116@Island";
        op_list->addEntityOp->PrefabContext = (char*)"defg";

        this->didSendAddEntityRequest = true;
    }
    else if (!this->didSendAddComponentRequest && didCreatePlayer) {
        char* buffer = new char[12] {
            // Bossa.Travellers.Ecs.Blueprint
            '\x8a', '\xf8', '\x03', '\x08', '\x0a', '\x06', '\x50', '\x6c', '\x61', '\x79', '\x65', '\x72' // identifier == Player
        };
        ClientObject* object;
        bool success = DeserializeComponent(8065, ClientObjectType::Snapshot, buffer, 12, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp = new AddComponentOp[41];
        op_list->addComponentLen = 41;

        op_list->addComponentOp[0].EntityId = 1;
        op_list->addComponentOp[0].InitialComponent.ComponentId = 8065;
        op_list->addComponentOp[0].InitialComponent.Object = object;
        
        delete[] buffer;
        buffer = new char[8] {
            // Bossa.Travellers.Weather.RadialStormState
            // weight 10
            //'\xaa', '\x4f', '\x05', '\x0d', '\x00', '\x00', '\x20', '\x41',
            // weight 1
            //'\xaa', '\x4f', '\x05', '\x0d', '\x00', '\x00', '\x80', '\x3f',
            // weight 0
            '\xaa', '\x4f', '\x05', '\x0d', '\x00', '\x00', '\x00', '\x00',
        };
        success = DeserializeComponent(1269, ClientObjectType::Snapshot, buffer, 8, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[1].EntityId = 1;
        op_list->addComponentOp[1].InitialComponent.ComponentId = 1269;
        op_list->addComponentOp[1].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[25] {
            // Bossa.Travellers.Weather.WeatherCellState
            // pressure 10
            //'\x9a', '\x47', '\x16', '\x15', '\x00', '\x00', '\x20', '\x41', '\x1a', '\x0f', '\x0d', '\x00', '\x00', '\x20', '\x41', '\x15', '\x00', '\x00', '\x20', '\x41', '\x1d', '\x00', '\x00', '\x20', '\x41'
            // pressure 200
            //'\x9a', '\x47', '\x16', '\x15', '\x00', '\x00', '\x48', '\x43', '\x1a', '\x0f', '\x0d', '\x00', '\x00', '\x20', '\x41', '\x15', '\x00', '\x00', '\x20', '\x41', '\x1d', '\x00', '\x00', '\x20', '\x41'
            // pressure 0
            '\x9a', '\x47', '\x16', '\x15', '\x00', '\x00', '\x00', '\x00', '\x1a', '\x0f', '\x0d', '\x00', '\x00', '\x20', '\x41', '\x15', '\x00', '\x00', '\x20', '\x41', '\x1d', '\x00', '\x00', '\x20', '\x41'
        };
        success = DeserializeComponent(1139, ClientObjectType::Snapshot, buffer, 25, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[2].EntityId = 1;
        op_list->addComponentOp[2].InitialComponent.ComponentId = 1139;
        op_list->addComponentOp[2].InitialComponent.Object = object;
        
        delete[] buffer;
        buffer = new char[46] {
            // Bossa.Travellers.Player.PlayerName
            '\xf2', '\x43', '\x2b', '\x0a', '\x0a', '\x73', '\x70', '\x30', '\x30', '\x6b', '\x74', '\x6f', '\x62', '\x65', '\x72', '\x12', '\x02', '\x69', '\x64', '\x1a', '\x04', '\x63', '\x55', '\x69', '\x64', '\x22', '\x0a', '\x62', '\x6f', '\x73', '\x73', '\x61', '\x54', '\x6f', '\x6b', '\x65', '\x6e', '\x2a', '\x07', '\x62', '\x6f', '\x73', '\x73', '\x61', '\x49', '\x64'
        };
        success = DeserializeComponent(1086, ClientObjectType::Snapshot, buffer, 46, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[3].EntityId = 1;
        op_list->addComponentOp[3].InitialComponent.ComponentId = 1086;
        op_list->addComponentOp[3].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[18] {
            // Bossa.Travellers.Inventory.InventoryState
            '\xca', '\x43', '\x0f', '\x08', '\x64', '\x12', '\x02', '\x7b', '\x7d', '\x28', '\xc8', '\x01', '\x30', '\x64', '\x40', '\x01', '\x48', '\x01'
        };
        success = DeserializeComponent(1081, ClientObjectType::Snapshot, buffer, 18, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[4].EntityId = 1;
        op_list->addComponentOp[4].InitialComponent.ComponentId = 1081;
        op_list->addComponentOp[4].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[91] {
            // Bossa.Travellers.Player.PlayerPropertiesState
            '\x82', '\x44', '\x58', '\x12', '\x13', '\x0a', '\x04', '\x48', '\x65', '\x61', '\x64', '\x12', '\x0b', '\x68', '\x61', '\x69', '\x72', '\x5f', '\x64', '\x72', '\x65', '\x61', '\x64', '\x73', '\x12', '\x1c', '\x0a', '\x04', '\x42', '\x6f', '\x64', '\x79', '\x12', '\x14', '\x74', '\x6f', '\x72', '\x73', '\x6f', '\x5f', '\x70', '\x6f', '\x6e', '\x63', '\x68', '\x6f', '\x56', '\x61', '\x72', '\x69', '\x61', '\x6e', '\x74', '\x42', '\x12', '\x11', '\x0a', '\x04', '\x46', '\x65', '\x65', '\x74', '\x12', '\x09', '\x6c', '\x65', '\x67', '\x73', '\x5f', '\x77', '\x72', '\x61', '\x70', '\x12', '\x0e', '\x0a', '\x04', '\x46', '\x61', '\x63', '\x65', '\x12', '\x06', '\x66', '\x61', '\x63', '\x65', '\x5f', '\x43', '\x20', '\x00'
        };
        success = DeserializeComponent(1088, ClientObjectType::Snapshot, buffer, 91, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[5].EntityId = 1;
        op_list->addComponentOp[5].InitialComponent.ComponentId = 1088;
        op_list->addComponentOp[5].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[30] {
            // Bossa.Travellers.Player.HealthState
            '\xaa', '\x43', '\x1b', '\x08', '\xc8', '\x01', '\x10', '\xc8', '\x01', '\x18', '\x01', '\x25', '\x00', '\x00', '\x00', '\x00', '\x28', '\x01', '\x32', '\x00', '\x3d', '\x00', '\x00', '\x80', '\x3f', '\x45', '\x00', '\x00', '\x80', '\x3f'
        };
        success = DeserializeComponent(1077, ClientObjectType::Snapshot, buffer, 30, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[6].EntityId = 1;
        op_list->addComponentOp[6].InitialComponent.ComponentId = 1077;
        op_list->addComponentOp[6].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[7] {
            // Bossa.Travellers.Inventory.WearableUtilsState
            '\x82', '\x50', '\x04', '\x0a', '\x00', '\x1a', '\x00'
        };
        success = DeserializeComponent(1280, ClientObjectType::Snapshot, buffer, 7, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[7].EntityId = 1;
        op_list->addComponentOp[7].InitialComponent.ComponentId = 1280;
        op_list->addComponentOp[7].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[61] {
            // Bossa.Travellers.Interact.InteractAgentState
            '\xda', '\x4b', '\x3a', '\x10', '\x01', '\x18', '\x00', '\x20', '\x00', '\x28', '\x00', '\x32', '\x0f', '\x0d', '\x00', '\x00', '\x00', '\x00', '\x15', '\x00', '\x00', '\x00', '\x00', '\x1d', '\x00', '\x00', '\x00', '\x00', '\x3a', '\x1b', '\x09', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x11', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x19', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x40', '\x01', '\x50', '\x01'
        };
        success = DeserializeComponent(1211, ClientObjectType::Snapshot, buffer, 61, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[8].EntityId = 1;
        op_list->addComponentOp[8].InitialComponent.ComponentId = 1211;
        op_list->addComponentOp[8].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[15] {
            // Bossa.Travellers.Interact.InteractAgentServerState
            '\xe2', '\x4b', '\x0c', '\x08', '\x00', '\x10', '\x00', '\x18', '\x01', '\x20', '\x00', '\x28', '\x00', '\x30', '\x00'
        };
        success = DeserializeComponent(1212, ClientObjectType::Snapshot, buffer, 15, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[9].EntityId = 1;
        op_list->addComponentOp[9].InitialComponent.ComponentId = 1212;
        op_list->addComponentOp[9].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[17] {
            // Bossa.Travellers.Alliance.AllianceNameState
            '\xe2', '\xb0', '\x03', '\x0d', '\x0a', '\x0b', '\x57', '\x41', '\x20', '\x41', '\x6c', '\x6c', '\x69', '\x61', '\x6e', '\x63', '\x65'
        };
        success = DeserializeComponent(6924, ClientObjectType::Snapshot, buffer, 17, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[10].EntityId = 1;
        op_list->addComponentOp[10].InitialComponent.ComponentId = 6924;
        op_list->addComponentOp[10].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[44] {
            // Bossa.Travellers.Alliance.AllianceAndCrewWorkerState
            '\xea', '\xb0', '\x03', '\x28', '\x0a', '\x14', '\x61', '\x6c', '\x6c', '\x69', '\x61', '\x6e', '\x63', '\x65', '\x5f', '\x69', '\x6e', '\x76', '\x61', '\x6c', '\x69', '\x64', '\x5f', '\x75', '\x69', '\x64', '\x12', '\x10', '\x63', '\x72', '\x65', '\x77', '\x5f', '\x69', '\x6e', '\x76', '\x61', '\x6c', '\x69', '\x64', '\x5f', '\x75', '\x69', '\x64'
        };
        success = DeserializeComponent(6925, ClientObjectType::Snapshot, buffer, 44, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[11].EntityId = 1;
        op_list->addComponentOp[11].InitialComponent.ComponentId = 6925;
        op_list->addComponentOp[11].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[3] {
            // Bossa.Travellers.Inventory.InventoryModificationState
            '\xd2', '\x43', '\x00'
        };
        success = DeserializeComponent(1082, ClientObjectType::Snapshot, buffer, 3, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[12].EntityId = 1;
        op_list->addComponentOp[12].InitialComponent.ComponentId = 1082;
        op_list->addComponentOp[12].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[4] {
            // new TransformHierarchyState.Data(new TransformHierarchyStateData(new List<Child> { }));
            '\xca', '\x88', '\x5d', '\x00'
        };
        success = DeserializeComponent(190601, ClientObjectType::Snapshot, buffer, 4, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[13].EntityId = 1;
        op_list->addComponentOp[13].InitialComponent.ComponentId = 190601;
        op_list->addComponentOp[13].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[89] {
            // Improbable.Corelibrary.Transforms.TransformState
            '\xd2', '\x88', '\x5d', '\x55', '\x0a', '\x06', '\x0a', '\x04', '\x00', '\xc8', '\x01', '\x00', '\x12', '\x05', '\x0d', '\x01', '\x00', '\x00', '\x00', '\x22', '\x1b', '\x09', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x11', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x19', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x2a', '\x0f', '\x0d', '\x00', '\x00', '\x00', '\x00', '\x15', '\x00', '\x00', '\x00', '\x00', '\x1d', '\x00', '\x00', '\x00', '\x00', '\x32', '\x0f', '\x0d', '\x00', '\x00', '\x00', '\x00', '\x15', '\x00', '\x00', '\x00', '\x00', '\x1d', '\x00', '\x00', '\x00', '\x00', '\x38', '\x00', '\x45', '\x00', '\x00', '\x00', '\x00'
        };
        success = DeserializeComponent(190602, ClientObjectType::Snapshot, buffer, 89, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[14].EntityId = 1;
        op_list->addComponentOp[14].InitialComponent.ComponentId = 190602;
        op_list->addComponentOp[14].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[4] {
            // Bossa.Travellers.Player.MountedGunShotState
            '\xe2', '\x95', '\x02', '\x00'
        };
        success = DeserializeComponent(4444, ClientObjectType::Snapshot, buffer, 4, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[15].EntityId = 1;
        op_list->addComponentOp[15].InitialComponent.ComponentId = 4444;
        op_list->addComponentOp[15].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[9] {
            // Bossa.Travellers.Controls.PilotState
            '\xaa', '\x45', '\x06', '\x08', '\x00', '\x10', '\x00', '\x18', '\x00'
        };
        success = DeserializeComponent(1109, ClientObjectType::Snapshot, buffer, 9, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[16].EntityId = 1;
        op_list->addComponentOp[16].InitialComponent.ComponentId = 1109;
        op_list->addComponentOp[16].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[5] {
            // Bossa.Travellers.Player.BuilderServerState
            '\xfa', '\x42', '\x02', '\x08', '\x00'
        };
        success = DeserializeComponent(1071, ClientObjectType::Snapshot, buffer, 5, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[17].EntityId = 1;
        op_list->addComponentOp[17].InitialComponent.ComponentId = 1071;
        op_list->addComponentOp[17].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[17] {
            // Bossa.Travellers.World.WorldData
            '\xda', '\x46', '\x0e', '\x08', '\x00', '\x15', '\x9a', '\x99', '\x19', '\x3e', '\x1d', '\x00', '\x00', '\x80', '\x3f', '\x20', '\x01'
        };
        success = DeserializeComponent(1131, ClientObjectType::Snapshot, buffer, 17, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[18].EntityId = 1;
        op_list->addComponentOp[18].InitialComponent.ComponentId = 1131;
        op_list->addComponentOp[18].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[10] {
            // Bossa.Travellers.Rope.RopeControlPoints
            '\xd2', '\x44', '\x07', '\x18', '\x00', '\x25', '\x00', '\x00', '\x00', '\x00'
        };
        success = DeserializeComponent(1098, ClientObjectType::Snapshot, buffer, 10, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[19].EntityId = 1;
        op_list->addComponentOp[19].InitialComponent.ComponentId = 1098;
        op_list->addComponentOp[19].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[5] {
            // Bossa.Travellers.Items.ShipHullAgentState
            '\xba', '\x4b', '\x02', '\x10', '\x00'
        };
        success = DeserializeComponent(1207, ClientObjectType::Snapshot, buffer, 5, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[20].EntityId = 1;
        op_list->addComponentOp[20].InitialComponent.ComponentId = 1207;
        op_list->addComponentOp[20].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[59] {
            // Bossa.Travellers.Analytics.PlayerAnalyticsState
            '\x8a', '\x7d', '\x38', '\x0a', '\x0b', '\x73', '\x6f', '\x6d', '\x65', '\x75', '\x73', '\x65', '\x72', '\x5f', '\x69', '\x64', '\x12', '\x0e', '\x73', '\x6f', '\x6d', '\x65', '\x73', '\x65', '\x73', '\x73', '\x69', '\x6f', '\x6e', '\x5f', '\x69', '\x64', '\x18', '\x00', '\x22', '\x05', '\x75', '\x6e', '\x69', '\x74', '\x79', '\x32', '\x0e', '\x64', '\x65', '\x66', '\x61', '\x75', '\x6c', '\x74', '\x50', '\x61', '\x79', '\x6c', '\x6f', '\x61', '\x64', '\x38', '\x00'
        };
        success = DeserializeComponent(2001, ClientObjectType::Snapshot, buffer, 59, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[21].EntityId = 1;
        op_list->addComponentOp[21].InitialComponent.ComponentId = 2001;
        op_list->addComponentOp[21].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[7] {
            // Bossa.Travellers.Scanning.KnowledgeServerState
            '\xa2', '\x53', '\x04', '\x08', '\x01', '\x20', '\x01'
        };
        success = DeserializeComponent(1332, ClientObjectType::Snapshot, buffer, 7, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[22].EntityId = 1;
        op_list->addComponentOp[22].InitialComponent.ComponentId = 1332;
        op_list->addComponentOp[22].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[11] {
            // Bossa.Travellers.Player.SchematicsLearnerClientState
            '\xba', '\x43', '\x08', '\x18', '\x0a', '\x20', '\x14', '\x28', '\x0a', '\x30', '\x0a'
        };
        success = DeserializeComponent(1079, ClientObjectType::Snapshot, buffer, 11, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[23].EntityId = 1;
        op_list->addComponentOp[23].InitialComponent.ComponentId = 1079;
        op_list->addComponentOp[23].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[10] {
            // Improbable.Corelibrary.Activation.Activated
            '\x92', '\xe3', '\x5c', '\x06', '\x08', '\x01', '\x10', '\x01', '\x18', '\x00'
        };
        success = DeserializeComponent(190002, ClientObjectType::Snapshot, buffer, 10, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[24].EntityId = 1;
        op_list->addComponentOp[24].InitialComponent.ComponentId = 190002;
        op_list->addComponentOp[24].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[16] {
            // Improbable.Corelib.Worker.Checkout.EntityLoadingControl
            '\x82', '\xe3', '\x5c', '\x0c', '\x08', '\x03', '\x10', '\x00', '\x18', '\x05', '\x20', '\x64', '\x28', '\x00', '\x32', '\x00'
        };
        success = DeserializeComponent(190000, ClientObjectType::Snapshot, buffer, 16, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[25].EntityId = 1;
        op_list->addComponentOp[25].InitialComponent.ComponentId = 190000;
        op_list->addComponentOp[25].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[10] {
            // Bossa.Travellers.Player.PlayerActivationState
            '\xf2', '\x47', '\x07', '\x08', '\x01', '\x10', '\xb9', '\x60', '\x18', '\x7b'
        };
        success = DeserializeComponent(1150, ClientObjectType::Snapshot, buffer, 10, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[26].EntityId = 1;
        op_list->addComponentOp[26].InitialComponent.ComponentId = 1150;
        op_list->addComponentOp[26].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[14] {
            // Bossa.Travellers.Ship.Lock.ShipyardVisitorState
            '\x9a', '\x4c', '\x0b', '\x08', '\x00', '\x12', '\x07', '\x61', '\x62', '\x63', '\x64', '\x65', '\x66', '\x67'
        };
        success = DeserializeComponent(1219, ClientObjectType::Snapshot, buffer, 14, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[27].EntityId = 1;
        op_list->addComponentOp[27].InitialComponent.ComponentId = 1219;
        op_list->addComponentOp[27].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[30] {
            // Bossa.Travellers.Craftingstation.CraftingStationClientState
            '\xea', '\x3e', '\x1b', '\x0a', '\x0b', '\x73', '\x63', '\x68', '\x65', '\x6d', '\x61', '\x74', '\x69', '\x63', '\x49', '\x64', '\x12', '\x05', '\x6f', '\x77', '\x6e', '\x65', '\x72', '\x28', '\x0c', '\x35', '\x00', '\x00', '\xf0', '\x41'
        };
        success = DeserializeComponent(1005, ClientObjectType::Snapshot, buffer, 30, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[28].EntityId = 1;
        op_list->addComponentOp[28].InitialComponent.ComponentId = 1005;
        op_list->addComponentOp[28].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[6] {
            // Bossa.Travellers.Player.NewPlayerState
            '\xba', '\xf7', '\x03', '\x02', '\x08', '\x01'
        };
        success = DeserializeComponent(8055, ClientObjectType::Snapshot, buffer, 6, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[29].EntityId = 1;
        op_list->addComponentOp[29].InitialComponent.ComponentId = 8055;
        op_list->addComponentOp[29].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[4] {
            // Bossa.Travellers.Player.PlayerBuffState
            '\xca', '\x8e', '\x02', '\x00'
        };
        success = DeserializeComponent(4329, ClientObjectType::Snapshot, buffer, 4, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[30].EntityId = 1;
        op_list->addComponentOp[30].InitialComponent.ComponentId = 4329;
        op_list->addComponentOp[30].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[4] {
            // Bossa.Travellers.World.FeedbackListener
            '\xe2', '\xf7', '\x03', '\x00'
        };
        success = DeserializeComponent(8060, ClientObjectType::Snapshot, buffer, 4, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[31].EntityId = 1;
        op_list->addComponentOp[31].InitialComponent.ComponentId = 8060;
        op_list->addComponentOp[31].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[18] {
            // Bossa.Travellers.Clock.FSimTimeState
            // time 40
            //'\xba', '\x44', '\x0f', '\x0d', '\x00', '\x00', '\x20', '\x42', '\x12', '\x06', '\x66', '\x73', '\x69', '\x6d', '\x49', '\x64', '\x18', '\x64'
            // time 0.15
            '\xba', '\x44', '\x0f', '\x0d', '\x9a', '\x99', '\x19', '\x3e', '\x12', '\x06', '\x66', '\x73', '\x69', '\x6d', '\x49', '\x64', '\x18', '\x64'
        };
        success = DeserializeComponent(1095, ClientObjectType::Snapshot, buffer, 18, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[32].EntityId = 1;
        op_list->addComponentOp[32].InitialComponent.ComponentId = 1095;
        op_list->addComponentOp[32].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[9] {
            // Improbable.Corelib.Metrics.ClientPhysicsLatency
            '\xe2', '\xf5', '\x5c', '\x05', '\x08', '\xfa', '\x01', '\x10', '\x64'
        };
        success = DeserializeComponent(190300, ClientObjectType::Snapshot, buffer, 9, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[33].EntityId = 1;
        op_list->addComponentOp[33].InitialComponent.ComponentId = 190300;
        op_list->addComponentOp[33].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[56] {
            // Bossa.Travellers.Devconsole.DevelopmentConsoleState
            '\xf2', '\x3e', '\x35', '\x08', '\x64', '\x10', '\x64', '\x1a', '\x0c', '\x67', '\x73', '\x69', '\x6d', '\x48', '\x6f', '\x73', '\x74', '\x6e', '\x61', '\x6d', '\x65', '\x22', '\x1b', '\x09', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x11', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x19', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x00', '\x2a', '\x04', '\x7a', '\x6f', '\x6e', '\x65'
        };
        success = DeserializeComponent(1006, ClientObjectType::Snapshot, buffer, 56, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[34].EntityId = 1;
        op_list->addComponentOp[34].InitialComponent.ComponentId = 1006;
        op_list->addComponentOp[34].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[30] {
            // Bossa.Travellers.Devconsole.FsimStatus
            '\x82', '\x3f', '\x1b', '\x0d', '\x00', '\x00', '\x70', '\x42', '\x10', '\xd2', '\x09', '\x18', '\x96', '\x01', '\x22', '\x0c', '\x66', '\x73', '\x69', '\x6d', '\x45', '\x6e', '\x67', '\x69', '\x6e', '\x65', '\x49', '\x64', '\x28', '\x7b'
        };
        success = DeserializeComponent(1008, ClientObjectType::Snapshot, buffer, 30, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[35].EntityId = 1;
        op_list->addComponentOp[35].InitialComponent.ComponentId = 1008;
        op_list->addComponentOp[35].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[66] {
            // Bossa.Travellers.Player.ClientAuthoritativePlayerState
            '\x8a', '\x43', '\x3f', '\x1a', '\x0f', '\x0d', '\x00', '\x00', '\x00', '\x00', '\x15', '\x00', '\x00', '\xc8', '\x42', '\x1d', '\x00', '\x00', '\x00', '\x00', '\x22', '\x14', '\x0d', '\x00', '\x00', '\x80', '\x3f', '\x15', '\x00', '\x00', '\x80', '\x3f', '\x1d', '\x00', '\x00', '\x80', '\x3f', '\x25', '\x00', '\x00', '\x80', '\x3f', '\x28', '\x02', '\x35', '\x00', '\x00', '\x80', '\x3f', '\x3d', '\x00', '\x00', '\xc8', '\x42', '\x42', '\x00', '\x48', '\x00', '\x50', '\x02', '\x58', '\x00', '\x60', '\x00', '\x68', '\x64'
        };
        success = DeserializeComponent(1073, ClientObjectType::Snapshot, buffer, 66, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[36].EntityId = 1;
        op_list->addComponentOp[36].InitialComponent.ComponentId = 1073;
        op_list->addComponentOp[36].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[14] {
            // Bossa.Travellers.Social.SocialWorkerId
            '\xea', '\xb2', '\x04', '\x0a', '\x0a', '\x08', '\x77', '\x6f', '\x72', '\x6b', '\x65', '\x72', '\x49', '\x64'
        };
        success = DeserializeComponent(9005, ClientObjectType::Snapshot, buffer, 14, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[37].EntityId = 1;
        op_list->addComponentOp[37].InitialComponent.ComponentId = 9005;
        op_list->addComponentOp[37].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[3] {
            // Bossa.Travellers.Misc.GamePropertiesState
            '\x82', '\x41', '\x00'
        };
        success = DeserializeComponent(1040, ClientObjectType::Snapshot, buffer, 3, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[38].EntityId = 1;
        op_list->addComponentOp[38].InitialComponent.ComponentId = 1040;
        op_list->addComponentOp[38].InitialComponent.Object = object;

        delete[] buffer;
        buffer = new char[4] {
            // Bossa.Travellers.Analytics.GsimEventAuditState
            '\xb2', '\xaf', '\x03', '\x00'
        };
        success = DeserializeComponent(6902, ClientObjectType::Snapshot, buffer, 4, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[39].EntityId = 1;
        op_list->addComponentOp[39].InitialComponent.ComponentId = 6902;
        op_list->addComponentOp[39].InitialComponent.Object = object;

        //---

        delete[] buffer;
        buffer = new char[20] {
            // Bossa.Travellers.Loot.IslandLightningTimerState
            '\xb2', '\x4e', '\x11', '\x08', '\x32', '\x10', '\x64', '\x18', '\xd2', '\x09', '\x20', '\xa8', '\x12', '\x28', '\x01', '\x30', '\x01', '\x3a', '\x01', '\x01'
        };
        success = DeserializeComponent(1254, ClientObjectType::Snapshot, buffer, 25, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));

        op_list->addComponentOp[40].EntityId = 2;
        op_list->addComponentOp[40].InitialComponent.ComponentId = 1254;
        op_list->addComponentOp[40].InitialComponent.Object = object;
        
        /*
        char* buffer = new char[1000];
        unsigned int length = 0;
        ClientObject* object = new ClientObject();
        object->Reference = 1; // injected by our mod, see ChangelogLoader_Patch.cs. Object is broken somehow, set breakpoint at ClientObjects.Dereference and set reference to 1 in debugger.

        bool success = SerializeComponent(1095, ClientObjectType::Snapshot, object, &buffer, &length);

        if (success) {
            Logger::Debug("IT WORKED!");
            Logger::Debug("length: " + length);
            Logger::Hexify(buffer, length);
        }
        else {
            Logger::Debug("FML!");
        }
        */

        this->didSendAddComponentRequest = true;
    }

    return op_list;
}

void Connection::SendAssetLoaded(AssetLoaded* asset_loaded) {
    Logger::Debug("ASSET LOADED: " + std::string(asset_loaded->Name) + " " + asset_loaded->Context);

    if (strcmp(asset_loaded->Name, "Traveller") == 0 && strcmp(asset_loaded->Context, "Player") == 0) {
        this->gameLoadedPlayer = true;
    }
}

bool Connection::DeserializeComponent(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj) {
    return this->vtable[component_id].Deserialize(component_id, objType, buffer, length, obj);
}

bool Connection::SerializeComponent(unsigned int component_id, ClientObjectType objType, ClientObject* obj, char** buffer, unsigned int* length) {
    return this->vtable[component_id].Serialize(component_id, objType, obj, buffer, length);
}

void Connection::SendComponentInterest(long entity_id, InterestOverride* interest_override, unsigned int interest_override_count)
{
    for (unsigned int i = 0; i < interest_override_count; i++) {
        
    }
    //
}
