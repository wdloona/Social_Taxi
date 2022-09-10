import { Form, Input, Button, Row, Col, Card, message } from 'antd';
import { CSSProperties, FC, ReactNode, useContext } from 'react';
import { NotificationContext, NotificationService } from '../../Services';
import { LoginType } from '../../Models';
import { LoginService } from '../../Services';


const Login: FC<{ children?: ReactNode, className?: string }> = ({ className, children }) => {

  const notifyContex = useContext(NotificationContext);

  const onFinish = async (values: any) => {

    message.loading("Авторизация", 0);

    const loginDto: LoginType = {
      Login: values.username,
      Password: values.password
    }

    LoginService.login(loginDto).then(async r => {
      let notificationService = new NotificationService(notifyContex);
      if (r.success) {
        await notificationService.Init();
        await notificationService.CheckSubscription();
        if (!notifyContex.isSubscribed) {
          await notificationService.SubscribeUser();
        }
        window.location.href = "/";
      }
      else {
        message.destroy();
        message.error(r.message);
      }
    }).catch((e) => {
      console.log(e);
      const { response } = e;
      message.destroy();
      return message.error(response, 5);
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };


  return (
    <div className="min-h-screen flex flex-col items-center justify-center p-5">

      <Form
        className='max-w-[380px] flex flex-col gap-6 p-5'
        name="basic"
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
        autoComplete="off"
        layout="vertical"
      >

        <div className="flex flex-col items-center gap-4 text-center">
          <svg width="150" height="60" viewBox="0 0 150 120" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path className='fill-slate-900' d="M58.4572 111.489V119.555H53.3251C49.6678 119.555 46.8166 118.721 44.7716 117.052C42.7267 115.346 41.7042 112.583 41.7042 108.764V96.4153H37.6929V88.5167H41.7042V80.9518H51.7914V88.5167H58.3982V96.4153H51.7914V108.875C51.7914 109.802 52.0273 110.47 52.4993 110.878C52.9712 111.286 53.7577 111.489 54.8588 111.489H58.4572Z" />
            <path className='fill-slate-900' d="M62.1293 103.98C62.1293 100.791 62.7585 97.9913 64.0169 95.581C65.3147 93.1706 67.0647 91.3164 69.267 90.0185C71.4693 88.7206 73.9272 88.0717 76.6407 88.0717C78.9609 88.0717 80.9862 88.5167 82.7166 89.4067C84.4863 90.2967 85.843 91.4648 86.7869 92.911V88.5167H96.874V119.555H86.7869V115.161C85.8037 116.607 84.4273 117.775 82.6576 118.665C80.9272 119.555 78.9019 120 76.5817 120C73.9075 120 71.4693 119.351 69.267 118.053C67.0647 116.718 65.3147 114.845 64.0169 112.435C62.7585 109.988 62.1293 107.169 62.1293 103.98ZM86.7869 104.036C86.7869 101.663 86.079 99.7899 84.6632 98.4178C83.2868 97.0457 81.5958 96.3597 79.5901 96.3597C77.5845 96.3597 75.8738 97.0457 74.4581 98.4178C73.0816 99.7528 72.3934 101.607 72.3934 103.98C72.3934 106.354 73.0816 108.245 74.4581 109.654C75.8738 111.026 77.5845 111.712 79.5901 111.712C81.5958 111.712 83.2868 111.026 84.6632 109.654C86.079 108.282 86.7869 106.409 86.7869 104.036Z" />
            <path className='fill-slate-900' d="M123.697 119.555L117.385 110.933L112.076 119.555H101.163L112.017 103.702L100.868 88.5167H112.194L118.506 97.0828L123.815 88.5167H134.728L123.697 104.147L135.023 119.555H123.697Z" />
            <path className='fill-slate-900' d="M144.042 85.2905C142.272 85.2905 140.817 84.8084 139.677 83.8443C138.576 82.843 138.025 81.6193 138.025 80.1731C138.025 78.6897 138.576 77.466 139.677 76.5019C140.817 75.5006 142.272 75 144.042 75C145.772 75 147.188 75.5006 148.289 76.5019C149.43 77.466 150 78.6897 150 80.1731C150 81.6193 149.43 82.843 148.289 83.8443C147.188 84.8084 145.772 85.2905 144.042 85.2905ZM149.056 88.5167V119.555H138.969V88.5167H149.056Z" />
            <path className='fill-lime-500' d="M17.9328 110.655L25.0705 88.5165H35.8066L24.1267 119.555H11.6799L0 88.5165H10.7951L17.9328 110.655Z" />
            <path className='fill-lime-500' fill-rule="evenodd" clip-rule="evenodd" d="M30 12C30 5.37258 35.3726 0 42 0H48C54.6274 0 60 5.37258 60 12V30H42C35.3726 30 30 24.6274 30 18V12ZM90 30H60V48C60 54.6274 65.3726 60 72 60H78C84.6274 60 90 54.6274 90 48V30ZM90 30V12C90 5.37258 95.3726 0 102 0H108C114.627 0 120 5.37258 120 12V18C120 24.6274 114.627 30 108 30H90Z" />
            <path className='fill-slate-900' d="M0 42C0 35.3726 5.37258 30 12 30H18C24.6274 30 30 35.3726 30 42V48C30 54.6274 24.6274 60 18 60H12C5.37258 60 0 54.6274 0 48V42Z" />
            <path className='fill-slate-900' d="M120 42C120 35.3726 125.373 30 132 30H138C144.627 30 150 35.3726 150 42V48C150 54.6274 144.627 60 138 60H132C125.373 60 120 54.6274 120 48V42Z" />
          </svg>

          <div className='text-2xl font-semibold text-slate-900'>Авторизация</div>
          <div className="text-base font-medium text-slate-500">Сервис популярного социально-валантерского такси</div>

        </div>

        <div className='flex flex-col gap-4'>
        <Form.Item
          required={true}
          label="Логин:"
          name="username"
          rules={[{ required: true, message: 'Пожалуйста, введите ваш логин!' }]}
        >
          <Input className='rounded-md text-sm font-medium h-10 px-4'/>
        </Form.Item>

        <Form.Item
          required={true}
          label="Пароль:"
          name="password"
          rules={[{ required: true, message: 'Пожалуйста, введите ваш пароль!' }]}
        >
          <Input.Password className='rounded-md text-sm font-medium h-10 px-4 gap-2' />
        </Form.Item>
        </div>

        <Form.Item >
          <Button type="primary" htmlType="submit" className='w-full h-10 rounded-md bg-lime-500 text-lime-50 text-sm font-medium border-0'>
            Войти
          </Button>
        </Form.Item>
      </Form>
    </div>
  )
}

export default Login;