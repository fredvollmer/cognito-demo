import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';

const IDP_HOSTNAME = 'https://icarus.auth.us-east-1.amazoncognito.com';
const OAUTH_HOSTNAME = 'https://cognito-idp.us-east-1.amazonaws.com/us-east-1_wi3kBOkom';

export function configureAuth(oidcConfigService: OidcConfigService) {
    return () =>
        oidcConfigService.withConfig({
            stsServer: OAUTH_HOSTNAME,
            redirectUrl: window.location.origin,
            postLoginRoute: '/fetch-data',
            // postLogoutRedirectUri: window.location.origin,
            forbiddenRoute: window.location.origin,
            clientId: '2vmjrkudt1srocep4p3f7ug5c0',
            scope: 'openid profile email',
            responseType: 'code',
            //silentRenew: true,
            //useRefreshToken: true,
            logLevel: LogLevel.Debug,

        }, {
          issuer: OAUTH_HOSTNAME,
          jwksUri: `${OAUTH_HOSTNAME}/.well-known/jwks.json`,
          authorizationEndpoint: `${IDP_HOSTNAME}/oauth2/authorize`,
          tokenEndpoint: `${IDP_HOSTNAME}/oauth2/token`,
          userinfoEndpoint: `${IDP_HOSTNAME}/oauth2/userInfo`,
          endSessionEndpoint: `${IDP_HOSTNAME}/logout`,
        });
}

@NgModule({
    imports: [AuthModule.forRoot()],
    providers: [
        OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configureAuth,
            deps: [OidcConfigService],
            multi: true,
        },
    ],
    exports: [AuthModule],
})

export class AuthConfigModule {}
