import { Categoria } from "";

export interface Tarefa {
  tarefaId?: string;
  titulo: string;
  descricao: string;
  status?: string;
  categoriaId: string;
  categoria?: Categoria;
  criadoEm?: string;
}