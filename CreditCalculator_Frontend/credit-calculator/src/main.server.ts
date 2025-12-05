import { renderModule } from '@angular/platform-server';
import { AppModule } from './app/app.module';

const bootstrap = () => renderModule(AppModule, {
  document: '<app-root></app-root>'
});

export default bootstrap;
