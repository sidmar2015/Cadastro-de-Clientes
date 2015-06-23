using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;


namespace CadastroDePessoas
{
    class ClienteDAO : DAO
    {
        //Cadastrar Livro - Insert
        public void Cadastrar(Cliente c)
        {
            try
            {
                Conectar();
                //Script do insert

                Cmd = new SqlCommand("insert into tb_cliente (nome, cpf, numero_1, numero_2, numero_3)values(@nome, @cpf, @numero_1, @numero_2, @numero_3)", Con);
                Cmd.Parameters.AddWithValue("@nome", c.nome);
                Cmd.Parameters.AddWithValue("@cpf", c.cpf);
                Cmd.Parameters.AddWithValue("@numero_1", c.numSorte[0]);
                Cmd.Parameters.AddWithValue("@numero_2", c.numSorte[1]);
                Cmd.Parameters.AddWithValue("@numero_3", c.numSorte[2]);

                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao Gravar Cliente: " + ex.Message);//lança uma exceção e mostra o tipo do erro ocorrido
            }
            finally
            {
                Desconetar();
            }
        }
        //Consultar todos os clientes cadastrados - Select * from...
        public List<Cliente> ConsultarTudo()
        {
            try
            {
                Conectar();
                Cmd = new SqlCommand("select * from tb_cliente", Con);//comando de select

                Dr = Cmd.ExecuteReader();
                List<Cliente> lista = new List<Cliente>();//cria uma lista vazia

                while (Dr.Read())//enquando houver dados a serem lidos...
                {
                    Cliente c = new Cliente();//cria a instancia do livro
                    //recupera os valores lidos da base de dados
                    c.id = Convert.ToInt32(Dr["id"]);
                    c.nome = Convert.ToString(Dr["nome"]);
                    c.cpf = Convert.ToString(Dr["cpf"]);
                    c.numSorte[1] = Convert.ToInt32(Dr["numero_1"]);
                    c.numSorte[2] = Convert.ToInt32(Dr["numero_2"]);
                    c.numSorte[3] = Convert.ToInt32(Dr["numero_3"]);

                    //adiciona a lista
                    lista.Add(c);
                }
                return lista;//retorna a lista de consultas
            }
            catch (Exception ex)
            {

                throw new Exception(" Nada foi encontrado" + ex.Message);
            }
            finally
            {
                Desconetar();
            }
        }
        //Exporta os dados para um arquivo .txt
        public void Exportar() {
            //cria o .txt
            using (System.IO.StreamWriter log = new System.IO.StreamWriter(@"C:\arquivos\log_clientes.txt"))
            {
                try
                {

                    Conectar();
                    Cmd = new SqlCommand("select * from tb_cliente", Con);//comando de select

                    Dr = Cmd.ExecuteReader();
                    List<Cliente> lista = new List<Cliente>();//cria uma lista vazia

                    while (Dr.Read())//enquando houver dados a serem lidos...
                    {
                        Cliente c = new Cliente();//cria a instancia do livro
                        //recupera os valores lidos da base de dados
                        c.id = Convert.ToInt32(Dr["id"]);
                        c.nome = Convert.ToString(Dr["nome"]);
                        c.cpf = Convert.ToString(Dr["cpf"]);
                        c.numSorte[0] = Convert.ToInt32(Dr["numero_1"]);
                        c.numSorte[1] = Convert.ToInt32(Dr["numero_2"]);
                        c.numSorte[2] = Convert.ToInt32(Dr["numero_3"]);

                        //adiciona a lista
                        lista.Add(c);
                    }


                    //aplica as regras de negócio para exportação
                    for (int i = 0; i < lista.Count; i++)
                    {
                        log.Write("01");        //escreve 01 no início da linha
                        log.Write(lista[i].cpf);//escreve o cpf após 01
                        //se o nome tiver menos de 40 caracteres
                        if (lista[i].nome.Length < 40)
                        {
                            while (lista[i].nome.Length < 40)
                            {
                                //adiciona espaços vazios até obter 40 caracteres
                                lista[i].nome += " ";
                            }
                            //escreve no arquivo
                            log.Write(lista[i].nome);
                            //se o nome tiver mais de 40 caracteres
                        }
                        else
                        {
                            for (int j = lista[i].nome.Length; j < 40; i--)
                            {
                                //remove os caracteres de tras para frente até obter 40 caracteres
                                lista[i].nome.Remove(j);
                            }
                            //escreve no arquivo
                            log.Write(lista[i].nome);
                        }
                        
                        //laço for para percorrer o array de números da sorte
                        //variável num percorre todas as ocorrencia de numeros da sorte de cada cliente
                        for (int num = 0; num < 3; num++)
                        {
                            log.WriteLine();
                            log.Write("02");
                            log.Write("00000" + (num + 1));
                            //verifica quantos caracteres o numero da sorte possui
                            int caracteres = lista[i].numSorte[num].ToString().Length;
                            //enquanto o numero da sorte não possuir 12 caracteres
                            for (int k = 0; k < (12 - caracteres);k++ )
                            {
                                //adiciona a qtde de 0 necessárias
                                log.Write("0");
                            }//fim for k   

                            //escreve o numero após completar com 0
                            log.Write(lista[i].numSorte[num]);
                            
                        }//fim for de números
                        //pula uma linha
                        log.WriteLine();
                    }//fim do for principal

                    //Fechando o arquivo
                    log.Close();
                    //Limpando a referencia dele da memória
                    log.Dispose();
                }
                catch (Exception ex)
                {

                    throw new Exception(" Nada foi encontrado" + ex.Message);
                }
                finally
                {
                    Desconetar();
                    
                }
            }
        }
        }
    }

