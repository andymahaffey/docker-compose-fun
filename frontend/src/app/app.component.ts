import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent { 
  private readonly cacheUrl ='api/cache/';

  response: any = null;
  getCacheForm = this.formBuilder.group({
    key: new FormControl(''),
  });

  putCacheForm = this.formBuilder.group({
    key: new FormControl(''),
    value: new FormControl('')
  });

  constructor(private http: HttpClient,
    private formBuilder: FormBuilder) { 

  }

  getCache(): void {    
    this.http.get(this.cacheUrl + this.getCacheForm.value['key']).subscribe((val) =>{
      this.response = val;
    });
  }

  putCache(): void {
    const body = {
      Value: this.putCacheForm.value['value']
    };
    const key = this.putCacheForm.value['key'];

    this.http.put(this.cacheUrl + key, body).subscribe(() =>{    
      this.putCacheForm.reset();
    });
  }
}
