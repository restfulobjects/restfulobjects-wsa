// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System.Collections.Generic;
using Microsoft.Practices.StoreApps.Infrastructure.Interfaces;

namespace RestfulObjects.WSA.Services
{
    public class DataRepository : IDataRepository
    {
        private const string UserEnteredData = "UserEnteredData";
        ISessionStateService _sessionStateService;

        public DataRepository(ISessionStateService sessionStateService)
        {
            _sessionStateService = sessionStateService;
        }

        public List<string> GetFeatures()
        {
            return new List<string>
            {
                "Application structuring with MVVM and dependencies",
                "Page navigation with ViewModel participation and navigation commanding",
                "Application state management through suspend, terminate, and resume",
                "User input validation on client and server side with validation error displays",
                "Loosely coupled communications with Commands and Pub/Sub events"
            };
        }

        public string GetUserEnteredData()
        {
            return _sessionStateService.SessionState.ContainsKey(UserEnteredData)
                ? _sessionStateService.SessionState[UserEnteredData] as string
                : string.Empty;
        }

        public void SetUserEnteredData(string data)
        {
            _sessionStateService.SessionState[UserEnteredData] = data;
        }
    }
}
