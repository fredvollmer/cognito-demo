import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public users: User[];
  public currentUser: User;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit(): void {
    this.http.get<User[]>(this.baseUrl + 'user', {
      headers: {
        Authorization: `Bearer ${this.oidcSecurityService.getToken()}`,
      },
    }).subscribe(result => {
      this.users = result;
    }, error => console.error(error));

    this.http.get<User>(this.baseUrl + 'whoami', {
      headers: {
        Authorization: `Bearer ${this.oidcSecurityService.getToken()}`,
      },
    }).subscribe(result => {
      this.currentUser = result;
    });
  }
}

interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  territoryId: string;
  role: string;
  isEnabled: boolean;
}
