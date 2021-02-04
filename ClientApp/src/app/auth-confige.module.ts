import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';

export function configureAuth(oidcConfigService: OidcConfigService) {
    return () =>
        oidcConfigService.withConfig({
            stsServer: 'https://cognito-idp.us-east-1.amazonaws.com/us-east-1_wi3kBOkom',
            redirectUrl: window.location.origin,
            postLoginRoute: '/fetch-data',
            // postLogoutRedirectUri: window.location.origin,
            forbiddenRoute: window.location.origin,
            clientId: '30ra7bclouj3bffmmg7okkanon',
            scope: 'openid profile email',
            responseType: 'code',
            silentRenew: true,
            useRefreshToken: true,
            logLevel: LogLevel.Debug,
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