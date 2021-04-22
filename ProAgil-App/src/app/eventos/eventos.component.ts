import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { empty } from 'rxjs';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventos: any = [];
  eventosFiltrados: any = [];
  _filtroLista: string = "";

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
   this._filtroLista = value;
   this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;

  }

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.getEventos();
  }
  // tslint:disable-next-line:typedef
  getEventos() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.eventos = response;
    }, error => {
      console.log(error);
    });
  }

  filtrarEventos(filtrarpor: string): any{
    filtrarpor = filtrarpor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarpor) !== -1
    );
  }

}
